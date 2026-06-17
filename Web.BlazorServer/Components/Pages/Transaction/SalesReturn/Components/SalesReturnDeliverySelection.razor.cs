using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Libraries.Entities;
using Shared.Libraries.Kernel;
using System.Collections.Immutable;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Transaction.Delivery;
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

    [Inject] IDeliveryHandler DeliveryHandler { get; set; } = default!;
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;

    AppDataGrid<DeliveryDataGridVM> DeliveryDataGrid { get; set; } = default!;
    DataGridSettings DeliveryDataGridSettings { get; set; } = new();

    string ActionGetDeliveries { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllDeliveries);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetDeliveries);
    AppFilterDescriptor? _searchFilter;
    List<int> _selection = [];

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
        _selection = [.. Document.DocumentLines.Select(d => d.BaseEntry).Where(x => x > 0)];
    }

    async Task LoadGridSettings()
    {
        await GridSettingsService.SetGridSettings(DeliveryDataGrid.DataGrid, settings => DeliveryDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await DeliveryDataGrid.DataGrid.ReloadSettings();
        await DeliveryDataGrid.DataGrid.Reload();
    }

    async Task<DataGridResultVM<DeliveryDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetDeliveries, true);
            var response = await DeliveryHandler.GetDeliveryDataGridAsync(intent);
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
                    Quantity = x.Quantity,
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
