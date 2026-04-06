using Microsoft.AspNetCore.Components;
using Shared.Kernel;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Transaction.InventoryCounting;
using Web.BlazorServer.ViewModels.Transaction.InventoryCounting;

namespace Web.BlazorServer.Components.Pages.Transaction.InventoryCounting;

public partial class InventoryCountingSheetViewPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter] public string? SheetNo { get; set; }

    [SupplyParameterFromQuery]
    [Parameter] public Guid? Document { get; set; }
    #endregion Parameters

    #region Injects
    [Inject] IInventoryCountingHandler InventoryCountingHandler { get; set; } = default!;
    #endregion Injects

    #region Primitives
    bool IsLoadingData => AppBusyService.IsBusy(ActionView);

    readonly string ActionView = EnumHelper.GetEnumDescription(AppActions.ViewInventoryCountingDocument);
    readonly string ActionIgnore = EnumHelper.GetEnumDescription(AppActions.IgnoreInventoryCountingSheet);
    #endregion Primitives

    #region Data Structures
    InventoryCountingSheetVM? Sheet { get; set; }
    #endregion Data Structures

    #region Overrides
    protected override void OnInitialized()
    {
        base.OnInitialized();
        AppBusyService.SetBusy(ActionView, true);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await LoadSheetAsync();
            await InvokeAsync(StateHasChanged);
        }
    }
    #endregion Overrides

    #region Custom Functions
    async Task LoadSheetAsync()
    {
        if (!Document.HasValue || string.IsNullOrWhiteSpace(SheetNo))
        {
            AppBusyService.SetBusy(ActionView, false);
            return;
        }

        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionView, true);
            return await InventoryCountingHandler.GetInventoryCountingDocumentAsync(Document.Value);
        }, AppActionOptionPresets.Loading(ActionView));

        AppBusyService.SetBusy(ActionView, false);

        action.OnSuccess(result =>
        {
            if (result is null)
            {
                ToastService.Error("Inventory Counting document not found.");
                return Task.CompletedTask;
            }

            Sheet = result.Sheets.FirstOrDefault(s => s.SheetNo.Value == SheetNo);

            if (Sheet is null)
                ToastService.Error($"Sheet {SheetNo} was not found in this document.");

            return Task.CompletedTask;
        });
    }

    async Task IgnoreSheet()
    {
        if (Sheet is null || !Document.HasValue) return;

        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionIgnore, true);
            return await InventoryCountingHandler.IgnoreInventoryCountingSheetAsync(Document.Value, SheetNo!);
        }, AppActionOptionPresets.Confirmed(ActionIgnore));

        AppBusyService.SetBusy(ActionIgnore, false);

        action.OnSuccess(async _ => await LoadSheetAsync());
    }

    void GoBack() =>
        NavManager.NavigateTo($"/transactions/inventory/inventory-counting/view?Id={Document}", true);
    #endregion Custom Functions
}
