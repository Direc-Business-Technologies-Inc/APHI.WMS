using Application.DataTransferObjects.Transactions.InventoryTransfer;
using Application.UseCases.Repositories.Integration.Transaction.InventoryTransfer;
using Integration.SAP.Entities.Transactional.InventoryTransfer;
using Mapster;
using MediatR;

namespace Application.UseCases.Queries.Transaction.InventoryTransfer
{
    public record GetInventoryTransferPostedRequestQry(int DocEntry) : IRequest<InventoryTransferHeaderDTO>;
    public class GetPostedInventoryTransferRequestQryHandler(
        IInventoryTransferIntegration inventoryTransferIntegration
    ): IRequestHandler<GetInventoryTransferPostedRequestQry, InventoryTransferHeaderDTO?>
    {
        public async Task<InventoryTransferHeaderDTO?> Handle(GetInventoryTransferPostedRequestQry request, CancellationToken cancellationToken)
        {
            InventoryTransferHeaderSAPDTO? headerResponse = await inventoryTransferIntegration.GetPostedInventoryTransferRequestHeaderAsync(request.DocEntry);
            if (headerResponse is null)
                return null;
            IEnumerable<InventoryTransferLineSAPDTO> linesResponse = await inventoryTransferIntegration.GetPostedInventoryTransferRequestLinesAsync(request.DocEntry);

            InventoryTransferHeaderDTO inventoryTransferRequestDTO = headerResponse.Adapt<InventoryTransferHeaderDTO>();
            IEnumerable<InventoryTransferLineDTO> inventoryTransferRequestLinesDTO = linesResponse.Adapt<IEnumerable<InventoryTransferLineDTO>>();

            inventoryTransferRequestDTO.Lines = [.. inventoryTransferRequestLinesDTO];

            return inventoryTransferRequestDTO;
        }
    }
}
