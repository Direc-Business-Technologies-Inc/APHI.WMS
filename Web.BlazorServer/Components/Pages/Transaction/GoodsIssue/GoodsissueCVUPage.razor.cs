using Mapster;
using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Entities;
using Shared.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.Handlers.Repositories.Transaction.GoodsIssue;
using Web.BlazorServer.Helpers;
using Web.BlazorServer.Services.Implementation;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Enums;
using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.GoodsIssue;

namespace Web.BlazorServer.Components.Pages.Transaction.GoodsIssue;

public partial class GoodsIssueCVUPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter]
    public int Ref { get; set; }

    [SupplyParameterFromQuery]
    [Parameter]
    public int Draft { get; set; }

    [Parameter]
    public bool ModalMode { get; set; } = false;

    [Parameter]
    public PageActionTypeEnum? ModalAction { get; set; } = null;

    #endregion Parameters

    #region Injects
    [Inject] IGoodsIssueHandler GoodsIssueHandler { get; set; } = default!;
    [Inject] IBusinessPartnerHandler BpHandler { get; set; } = default!;
    [Inject] ITransactionTypeHandler TransTypeHandler { get; set; } = default!;
    [Inject] IWarehouseMasterDataHandler WarehouseHandler { get; set; } = default!;
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    #endregion Injects

    #region Primitives
    PageActionTypeEnum PageAction { get; set; }

    bool Creating => PageAction == PageActionTypeEnum.Create;
    bool Viewing => PageAction == PageActionTypeEnum.View;
    bool IsBusy => AppBusyService.IsBusy(ActionGetGoodsIssue) || AppBusyService.IsBusy(ActionCreateGoodsIssue);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetGoodsIssue);

    readonly string ActionGetGoodsIssue = EnumHelper.GetEnumDescription(AppActions.ViewGoodsIssue);
    readonly string ActionGetTransactionTypes = EnumHelper.GetEnumDescription(AppActions.GetVendors);
    readonly string ActionGetWarehouses = EnumHelper.GetEnumDescription(AppActions.GetWarehouses);
    readonly string ActionCreateGoodsIssue = EnumHelper.GetEnumDescription(AppActions.CreateGoodsIssue);

    int BusinessPartnersCount { get; set; } = 0;
    int WarehousesCount { get; set; } = 0;

    #endregion Primitives

    #region Data Structures
    AppTable<GoodsIssueLineVM> GoodsIssueTable { get; set; } = default!;
    DataGridSettings GoodsIssueTableSettings { get; set; } = new();
    List<BusinessPartnerVM> BusinessPartners { get; set; } = [];
    List<WarehouseVM> Warehouses { get; set; } = [];
    List<TransactionTypeVM> TransactionTypes { get; set; } = [];

    public IDataGridIntentAdapter DatagridAdapter { get; set; } = default!;
    #endregion Data Structures

    #region Overrides
    protected override void OnParametersSet()
    {
        if (!ModalMode)
            PageAction = PageActionHelper.GetPageActionType(NavManager.Uri);
        else if (ModalMode && ModalAction != null)
            PageAction = (PageActionTypeEnum)ModalAction;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        AppBusyService.SetBusy(ActionGetGoodsIssue, true);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await LoadDataAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected override Task CancelEditing()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleSubmit()
    {
        if (FormData.DocumentLines.Count() <= 0)
        {
            ToastService.Warning("Please select Items to Issue");
            return;
        }

        if (FormData.DocumentLines.Any(x => x.Quantity <= 0))
        {
            if (!await AlertService.PromptAsync("Some Items in the Goods Issue has no Quantity. These Items will be removed in the transaction. Are you sure wou want to proceed?"))
                return;
            FormData.DocumentLines = [.. FormData.DocumentLines.ToList().Where(x => x.Quantity > 0)];
        }

        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionCreateGoodsIssue, true);

            bool response = await GoodsIssueHandler.PostGoodsIssueAsync(FormData);

            return response;
        }, AppActionOptionPresets.Confirmed(ActionCreateGoodsIssue));

        action.OnSuccess(async (args) =>
        {
            NavManager.NavigateTo("/transactions/inventory/goods-issue/?t=pndng");
        });
    }

    protected override Task InitializeEditing()
    {
        throw new NotImplementedException();
    }

    #endregion Overrides

    #region Custom Functions

    async Task LoadDataAsync()
    {
        if (!AppBusyService.IsBusy(ActionGetGoodsIssue))
        {
            AppBusyService.SetBusy(ActionGetGoodsIssue, true);
            await InvokeAsync(StateHasChanged);
            await Task.Yield();
        }

        await Task.WhenAll(
            GetGoodsIssue(),
            LoadTransactionTypes(),
            LoadWarehouses(new()));

        FormData.PreparedBy = AuthenticationService.GetUserName();

        AppBusyService.SetBusy(ActionGetGoodsIssue, false);
        await InvokeAsync(StateHasChanged);
        await Task.Yield();

        if (!GridSettingsLoaded && !IsLoadingData)
            await LoadGridSettings();
    }

    async Task GetGoodsIssue()
    {
        if (Creating)
            return;

        var action = await AppActionFactory.RunAsync(async () =>
        {
            GoodsIssueVM? result = null;
            if (Draft == 1)
                result = await GoodsIssueHandler.GetDraftGoodsIssueAsync(Ref);
            else
                result = await GoodsIssueHandler.GetGoodsIssueAsync(Ref);

            AppBusyService.SetBusy(ActionGetGoodsIssue, false);
            return result;

        }, AppActionOptionPresets.Loading(ActionGetGoodsIssue));

        action.OnSuccess(async (args) =>
        {
            if (action.Result is null)
                ToastService.Error("Goods Issue not found");
            else
            {
                action.Result.Adapt(FormData);

                FormData.PreparedBy = AuthenticationService.GetUserName();
            }
        });
    }

    async Task LoadGridSettings()
    {
        await GridSettingsService.SetGridSettings(GoodsIssueTable.DataGrid, settings => GoodsIssueTableSettings = settings ?? new());
        GridSettingsLoaded = true;

        await GoodsIssueTable.DataGrid.ReloadSettings();
        await GoodsIssueTable.DataGrid.Reload();
    }

    async Task Issue()
    {
        if (UnsavedChangesService.HasChanges && Creating)
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Goods Issue Creation"))
                return;

        NavManager.NavigateTo($"/transactions/inventory/goods-issue/?t=pndng", true);
    }

    async Task LoadTransactionTypes()
    {

        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetTransactionTypes, true);


            TransactionTypes = [.. await TransTypeHandler.GetTransactionTypesAsync()];

            AppBusyService.SetBusy(ActionGetTransactionTypes, false);
        }, AppActionOptionPresets.Loading(ActionGetTransactionTypes));
    }

    async Task LoadWarehouses(LoadDataArgs args)
    {

        var action = await AppActionFactory.RunAsync(async () =>
        {
            await Task.Yield();

            AppBusyService.SetBusy(ActionGetWarehouses, true);

            DatagridAdapter = new DataGridIntentAdapter(args);
            DatagridAdapter.AdaptToPagination();
            if (DatagridAdapter.QueryIntent.Take <= 0)
                DatagridAdapter.QueryIntent.Take = 5;

            if (!string.IsNullOrEmpty(args.Filter))
                DatagridAdapter.QueryIntent.Filters.Add(new()
                {
                    LogicalOperator = LogicalOperatorEnum.AND,
                    Property = nameof(WarehouseVM.WhsName),
                    Value = args.Filter,
                    ComparisonOperator = ComparisonOperatorEnum.Contains
                });

            (IEnumerable<WarehouseVM> Data, int Count) = await WarehouseHandler.GetWarehousesAsync(DatagridAdapter.QueryIntent);

            Warehouses = [.. Data];
            WarehousesCount = Count;

            AppBusyService.SetBusy(ActionGetWarehouses, false);

            await InvokeAsync(StateHasChanged);
        }, AppActionOptionPresets.Loading(ActionGetWarehouses));
    }

    void OnWarehouseChange(WarehouseVM? warehouse)
    {
        OnFieldChanged(nameof(FormData.Warehouse));

        if (warehouse is null)
            return;

        foreach (GoodsIssueLineVM item in FormData.DocumentLines)
            item.Warehouse = warehouse;
    }

    void OnTransactionTypeChange(TransactionTypeVM? transType)
    {
        OnFieldChanged(nameof(FormData.TransactionType));

        if (transType is null)
            return;
    }

    #endregion Custom Functions
}
