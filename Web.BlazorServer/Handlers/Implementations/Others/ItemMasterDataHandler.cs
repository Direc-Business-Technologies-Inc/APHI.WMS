using Application.DataTransferObjects.Transactions.Commons;
using Application.UseCases.Queries.Others;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.Handlers.Implementations.Others;

public class ItemMasterDataHandler(
    ISender Sender) 
    : IItemMasterDataHandler
{
    public async Task<(IEnumerable<ItemVM> Data, int Count)> GetMerchandiseItemsAsync(DataGridIntent intent)
    {
        GetMerchandiseItemsQry qry = new(intent);
        (IEnumerable<ItemDTO> Data, int Count) = await Sender.Send(qry);

        return (Data.Adapt<IEnumerable<ItemVM>>(), Count);
    }
}
