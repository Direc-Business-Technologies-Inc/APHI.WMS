using Mapster;
using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Transaction.Receiving;
using Web.BlazorServer.Helpers;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Enums;
using Web.BlazorServer.ViewModels.System;
using Web.BlazorServer.ViewModels.Transaction.Receiving;

namespace Web.BlazorServer.Components.Pages.Transaction.Receiving;

public partial class GoodsReceiptPOCVUPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter]
    public int Ref { get; set; }

    [Parameter]
    public bool ModalMode { get; set; } = false;

    [Parameter]
    public PageActionTypeEnum? ModalAction { get; set; } = null;

    #endregion Parameters

    #region Injects
    [Inject] IReceivingHandler ReceivingHandler { get; set; } = default!;
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    #endregion Injects

    #region Primitives
    PageActionTypeEnum PageAction { get; set; }

    bool PasswordVisibility { get; set; } = false;
    bool Creating => PageAction == PageActionTypeEnum.Create;
    bool Viewing => PageAction == PageActionTypeEnum.View;
    bool IsBusy => AppBusyService.IsBusy(ActionGetPurchaseDeliveryNote);
    bool IsLoadingData => AppBusyService.IsBusy(ActionGetPurchaseDeliveryNote);

    readonly string ActionGetPurchaseDeliveryNote = EnumHelper.GetEnumDescription(AppActions.ViewPurchaseDeliveryNote);

    #endregion Primitives

    #region Data Structures
    AppTable<PurchaseDeliveryNoteLineVM> PurchaseDeliveryNoteTable { get; set; } = default!;
    DataGridSettings PurchaseDeliveryNoteTableSettings { get; set; } = new();
    List<NavigationRouteVM> AdditionalRoutes { get; set; } = [new() {
        Name = "Goods Receipt Purchase Order",
        Position = 0,
        Icon = "assignment",
    }];
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
        AppBusyService.SetBusy(ActionGetPurchaseDeliveryNote, true);
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

    protected override async Task InitializeEditing()
    {
        throw new NotImplementedException();
    }

    protected override async Task CancelEditing()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleSubmit()
    {
        throw new NotImplementedException();
    }

    #endregion Overrides

    #region Custom Function

    async Task LoadDataAsync()
    {
        if (!AppBusyService.IsBusy(ActionGetPurchaseDeliveryNote))
        {
            AppBusyService.SetBusy(ActionGetPurchaseDeliveryNote, true);
            await InvokeAsync(StateHasChanged);
            await Task.Yield();
        }

        await Task.WhenAll(
            GetPurchaseDeliveryNote());

        await InvokeAsync(StateHasChanged);
        await Task.Yield();

        if (!GridSettingsLoaded && !IsLoadingData)
            await LoadGridSettings();
    }

    async Task GetPurchaseDeliveryNote()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {

            var result = await ReceivingHandler.GetPurchaseDeliveryNoteAsync(Ref);

            AppBusyService.SetBusy(ActionGetPurchaseDeliveryNote, false);
            return result;

        }, AppActionOptionPresets.Loading(ActionGetPurchaseDeliveryNote));

        action.OnSuccess(async (args) =>
        {
            if (action.Result is null)
                ToastService.Error("Purchase Order not found");
            else
            {
                action.Result.Adapt(FormData);

                FormData.ReceivedBy = AuthenticationService.GetUserName();
            }
        });
    }

    async Task LoadGridSettings()
    {
        await GridSettingsService.SetGridSettings(PurchaseDeliveryNoteTable.DataGrid, settings => PurchaseDeliveryNoteTableSettings = settings ?? new());
        GridSettingsLoaded = true;

        await PurchaseDeliveryNoteTable.DataGrid.ReloadSettings();
        await PurchaseDeliveryNoteTable.DataGrid.Reload();
    }

    async Task Return() => NavManager.NavigateTo($"/transactions/purchasing/receiving?t=grpo", true);

    #endregion Custom Function
}
