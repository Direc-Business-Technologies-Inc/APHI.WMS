using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Libraries.Entities;
using Shared.Libraries.Kernel;
using Web.BlazorServer.Components.Shared.Abstraction;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Transaction.Delivery;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Abstraction;
using Web.BlazorServer.ViewModels.Transaction.Delivery;

namespace Web.BlazorServer.Components.Pages.Transaction.Delivery.Components;

public partial class DeliveryDraftDataGrid
{

    [Inject] IDeliveryHandler DeliveryHandler { get; set; } = default!;
    [Inject] IGridSettingsService GridSettingsService { get; set; } = default!;
    [Parameter] public List<AppFilterDescriptor> Filters { get; set; } = [];

    AppDataGrid<DeliveryDataGridVM> DeliveryGrid { get; set; } = default!;
    DataGridSettings DeliveryDataGridSettings { get; set; } = new();

    string ActionGetAllDeliveries { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllDeliveries);
    AppFilterDescriptor? _searchFilter;

    async Task<DataGridResultVM<DeliveryDataGridVM>> LoadDataAsync(DataGridIntent intent)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetAllDeliveries, true);

            if (Filters.Count > 0) intent.Filters.AddRange(Filters);

            var response = await DeliveryHandler.GetDeliveryDraftDataGridAsync(intent);

            return response;

        }, AppActionOptionPresets.Loading(ActionGetAllDeliveries));

        AppBusyService.SetBusy(ActionGetAllDeliveries, false);
        return DataGridResultVM<DeliveryDataGridVM>.New(action.Result.Data ?? [], action.Result.Count);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await LoadGridSettings();
            await InvokeAsync(StateHasChanged);
        }
    }

    async Task LoadGridSettings()
    {
        await GridSettingsService.SetGridSettings(DeliveryGrid.DataGrid, settings => DeliveryDataGridSettings = settings ?? new());
        GridSettingsLoaded = true;

        await DeliveryGrid.DataGrid.ReloadSettings();
        await DeliveryGrid.DataGrid.Reload();
    }

    async Task OnSearchAsync() => await DeliveryGrid.DataGrid.Reload();

    void ViewDelivery(DeliveryDataGridVM delivery) => NavManager.NavigateTo($"/transactions/sales/delivery/view?ref={delivery.DocEntry}&draft=true");
}
