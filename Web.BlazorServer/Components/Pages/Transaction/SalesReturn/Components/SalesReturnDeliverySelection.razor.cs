using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Libraries.Entities;
using Shared.Libraries.Kernel;
using System.Collections.Immutable;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Implementations.Others;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.Handlers.Repositories.Transaction.Delivery;
using Web.BlazorServer.Services.Implementation;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Abstraction;
using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Delivery;
using Web.BlazorServer.ViewModels.Transaction.SalesReturn;

namespace Web.BlazorServer.Components.Pages.Transaction.SalesReturn.Components;

public partial class SalesReturnDeliverySelection
{
    [Parameter] public SalesReturnVM Document { get; set; } = new();
    [Parameter] public EventCallback<SalesReturnVM> DocumentChanged { get; set; } = new();
    [Parameter] public string? CardCode { get; set; } = null;

    [Inject] IDeliveryHandler DeliveryHandler { get; set; } = default!;
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    [Inject] ISchoolYearHandler SchoolYearHandler { get; set; } = default!;
    IDataGridIntentAdapter DatagridAdapter { get; set; } = default!;

    AppDataGrid<DeliveryDataGridVM> DeliveryDataGrid { get; set; } = default!;
    DataGridSettings DeliveryDataGridSettings { get; set; } = new();
    SchoolYearVM? SchoolYearFilter = null;
    string ActionGetDeliveries { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllDeliveries);
    string ActionGetSchoolYears { get; } = EnumHelper.GetEnumDescription(AppActions.GetSchoolYears);

    bool IsLoadingData => AppBusyService.IsBusy(ActionGetDeliveries);
    AppFilterDescriptor? _searchFilter;
    List<int> _selection = [];
    List<SchoolYearVM> SchoolYears = [];
    int SchoolYearsCount = 0;

    const int SY_START_MONTH = 6;
    const int SY_START_DAY = 8;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await LoadGridSettings();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DateTime syStart = new DateTime(
            DateTime.Now.Year, 
            SY_START_MONTH, 
            SY_START_DAY);

        string syCode = DateTime.Now < syStart ? 
            $"{syStart.Year - 1}-{syStart.Year}" :
            $"{syStart.Year}-{syStart.Year + 1}";

        SchoolYearFilter = new()
        {
            Code = syCode,
            Name = syCode,
            U_YearFrom = "0",
            U_YearTo = "0",
        };
        
        _selection = [.. Document.DocumentLines.Select(d => d.BaseEntry).Where(x => x > 0)];

    }

    async Task LoadGridSettings()
    {
        await GridSettingsService.SetGridSettings(DeliveryDataGrid.DataGrid, settings => DeliveryDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await DeliveryDataGrid.DataGrid.ReloadSettings();
        await DeliveryDataGrid.DataGrid.Reload();
    }

    async Task LoadSchoolYears(LoadDataArgs args)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            await Task.Yield();

            AppBusyService.SetBusy(ActionGetSchoolYears, true);

            DatagridAdapter = new DataGridIntentAdapter(args);
            DatagridAdapter.AdaptToPagination();
            if (DatagridAdapter.QueryIntent.Take <= 0)
                DatagridAdapter.QueryIntent.Take = 5;

            if (!string.IsNullOrEmpty(args.Filter))
                DatagridAdapter.QueryIntent.Filters.Add(new()
                {
                    LogicalOperator = LogicalOperatorEnum.AND,
                    Property = nameof(SchoolYearVM.Code),
                    Value = args.Filter,
                    ComparisonOperator = ComparisonOperatorEnum.Contains
                });

            (IEnumerable<SchoolYearVM> Data, int Count) = await SchoolYearHandler.GetSchoolYearsAsync(DatagridAdapter.QueryIntent);

            SchoolYears = [.. Data];
            SchoolYearsCount = Count;

            AppBusyService.SetBusy(ActionGetSchoolYears, false);

            await InvokeAsync(StateHasChanged);
        }, AppActionOptionPresets.Loading(ActionGetSchoolYears));
    }


    async Task<DataGridResultVM<DeliveryDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetDeliveries, true);

            intent.Filters.Add(
                new AppFilterDescriptor
                {
                    Value = "O",
                    ComparisonOperator = ComparisonOperatorEnum.Equals,
                    Property = "DocStatus"
                });
            if (SchoolYearFilter != null)
            {
                intent.Filters.Add(
                    new AppFilterDescriptor
                    {
                        Value = SchoolYearFilter.Name,
                        ComparisonOperator = ComparisonOperatorEnum.Equals,
                        Property = "SchoolYear"
                    });
            }

            var response = CardCode is null ?
                await DeliveryHandler.GetDeliveryDataGridAsync(intent) :
                await DeliveryHandler.GetDeliveryByCustomerDataGridAsync(intent, CardCode);
            return response;
        }, AppActionOptionPresets.Loading(ActionGetDeliveries));

        AppBusyService.SetBusy(ActionGetDeliveries, false);
        return DataGridResultVM<DeliveryDataGridVM>.New(action.Result.Data ?? [], action.Result.Count);
    }

    void ToggleSelection(int entry)
    {
        if (_selection.Contains(entry)) _selection.Remove(entry);
        else _selection.Add(entry);
    }
    async Task OnSearchAsync() => await DeliveryDataGrid.DataGrid.Reload();

    async Task Select()
    {
        if (_selection.Count == 0)
        {
            Document.DocumentLines.Clear();
            DialogService.Close(false);
            return;
        }
        var action = await AppActionFactory.RunAsync(async () =>
        {
            List<DeliveryVM?> deliveryVMs = await DeliveryHandler.GetDeliveriesAsync([.. _selection]);

            deliveryVMs.RemoveAll(x => x is null);
            List<int> selectedIds = [.. deliveryVMs.Select(d => d?.SapReference.DocEntry ?? -1).Where(x => x > 0)];
            List<int> existingIds = [.. Document.DocumentLines.Select(d => d.BaseEntry).Where(x => x > 0)];
            deliveryVMs.RemoveAll(x => x is null ? true : existingIds.Contains(x.SapReference.DocEntry ?? 0));

            Document.DRNo = string.Join(", ", selectedIds);
            Document.DocumentLines.RemoveAll(d => !selectedIds.Contains(d.BaseEntry));
            foreach (var item in deliveryVMs)
            {
                if (item is null) continue;

                Document.DocumentLines.AddRange(item.DocumentLines.Select((x, ix) => new SalesReturnLineVM
                {
                    DRNo = item.SapReference?.DocEntry?.ToString() ?? "0",
                    LineNum = ix + 1,
                    BaseEntry = item.SapReference.DocEntry ?? 0,
                    BaseDocNum = item.SapReference.DocNum ?? 0,
                    BaseLine = x.LineNum,
                    ItemCode = x.ItemCode,
                    ItemName = x.ItemName,
                    UoMCode = x.UoMCode,
                    UoMName = x.UoMName,
                    UoMValue = x.UoMValue,
                    TargetQuantity = x.Quantity,
                    OpenQuantity = x.Quantity,
                    Quantity = 0, // start in zero for scanning or input
                    ISBN = x.ISBN,
                    Warehouse = x.Warehouse is null ? null : new WarehouseVM
                    {
                        WhsCode = x.Warehouse.WhsCode,
                        WhsName = x.Warehouse.WhsName
                    },
                }));
            }
        }, AppActionOptionPresets.Loading(ActionGetDeliveries));
        AppBusyService.SetBusy(ActionGetDeliveries, false);

        await InvokeAsync(StateHasChanged);
        await DocumentChanged.InvokeAsync(Document);
        DialogService.Close(true);
    }
}
