using Shared.Libraries.Entities;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Repositories.Others;

public interface IDashboardItemsHandler
{
    Task<DashboardItemsVM> GetDashboardItemsAsync();
}
