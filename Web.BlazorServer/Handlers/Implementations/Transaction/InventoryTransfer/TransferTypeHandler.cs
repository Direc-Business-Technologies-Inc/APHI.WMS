using Application.DataTransferObjects.Others;
using Application.UseCases.Queries.Others;
using Mapster;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Transaction.InventoryTransfer;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Implementations.Transaction.InventoryTransfer;

public class TransferTypeHandler(ISender sender) : ITransferTypeHandler
{
    public async Task<IEnumerable<TransferTypeVM>> GetTransferTypesAsync()
    {
        GetTransferTypeQry query = new();
        IEnumerable<TransferTypeDTO> result = await sender.Send(query);
        return result.Adapt<IEnumerable<TransferTypeVM>>();
    }
}
