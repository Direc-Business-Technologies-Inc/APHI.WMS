using Shared.Libraries.Entities;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Repositories.Others;

public interface IWarehouseMasterDataHandler
{
    Task<(IEnumerable<WarehouseVM> Data, int Count)> GetWarehousesAsync(DataGridIntent intent);
}
