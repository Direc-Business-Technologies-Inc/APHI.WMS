using Shared.Entities;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.Handlers.Repositories.Others;

public interface IItemMasterDataHandler
{
    Task<(IEnumerable<ItemVM> Data, int Count)> GetMerchandiseItemsAsync(DataGridIntent intent);
}
