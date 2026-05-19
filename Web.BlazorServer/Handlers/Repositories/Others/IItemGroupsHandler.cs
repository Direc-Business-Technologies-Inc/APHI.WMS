using Shared.Libraries.Entities;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Repositories.Others
{
    public interface IItemGroupsHandler
    {
        Task<(IEnumerable<ItemGroupVM> Data, int Count)> GetItemGroupsAsync(DataGridIntent intent);
    }
}
