using Application.DataTransferObjects.Others;
using Application.UseCases.Queries.Others;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Implementations.Others;

public class ItemGroupsHandler(
    ISender Sender)
    : IItemGroupsHandler
{
    public async Task<(IEnumerable<ItemGroupVM> Data, int Count)> GetItemGroupsAsync(DataGridIntent intent)
    {
        GetItemGroupsQry qry = new(intent);
        (IEnumerable<ItemGroupDTO> Data, int Count) = await Sender.Send(qry);

        return (Data.Adapt<IEnumerable<ItemGroupVM>>(), Count);

    }
}
