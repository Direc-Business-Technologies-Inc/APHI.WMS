using Application.UseCases.Queries.Others;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Shared.Libraries.Kernel;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Implementations.Others;
using Web.BlazorServer.Handlers.Implementations.Transaction.SalesReturn;
using Web.BlazorServer.Handlers.Repositories.Others;

namespace Web.BlazorServer.Components.Pages.Dashboard;


public partial class DashboardPage
{
    public int DeliveryCount = 0;
    public int ReturnsCount = 0;
    public int ReceivingCount = 0;

    public int DeliveryTodayCount = 0;
    public int ReturnsTodayCount = 0;
    public int ReceivingTodayCount = 0;

    readonly string ActionGetDashboard = EnumHelper.GetEnumDescription(AppActions.GetDashboardItems);
    public bool IsBusy => AppBusyService.IsBusy(ActionGetDashboard);
    [Inject] IDashboardItemsHandler DashboardItemsHandler { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender) await GetDashboardItems();
    }

    async Task GetDashboardItems()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            await InvokeAsync(StateHasChanged);
            var result = await DashboardItemsHandler.GetDashboardItemsAsync();
            AppBusyService.SetBusy(ActionGetDashboard, false);
            return result;
        }, AppActionOptionPresets.Loading(ActionGetDashboard));

        action.OnSuccess(async (args) =>
        {
            if (action.Result is null)
                ToastService.Error("Error while retrieving dashboard items");
            else
            {
                DeliveryCount = action.Result.DeliveryCount;
                ReturnsCount = action.Result.ReturnCount;
                ReceivingCount = action.Result.ReceivingCount;
                DeliveryTodayCount = action.Result.DeliveryTodayCount;
                ReceivingTodayCount = action.Result.ReceivingTodayCount;
                ReturnsTodayCount = action.Result.ReturnTodayCount;
                await InvokeAsync(StateHasChanged);
            }
        });
    }
}
