using Application.DataTransferObjects.Transactions.Receiving;
using Application.UseCases.Repositories.Integration.Transaction.Receiving;
using Integration.SAP.Entities.Transactional.Receiving;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.Receiving;

public record GetGRPODraftQry(int DocEntry) : IRequest<PurchaseDeliveryNoteDTO?>;

public class GetGRPODraftQryHandler(
    IReceivingIntegration receivingIntegration)
    : IRequestHandler<GetGRPODraftQry, PurchaseDeliveryNoteDTO?>
{
    public async Task<PurchaseDeliveryNoteDTO?> Handle(GetGRPODraftQry request, CancellationToken cancellationToken)
    {
        PurchaseDeliveryNoteHeaderSAPDTO? headerResponse = await receivingIntegration.GetGRPODraftHeaderAsync(request.DocEntry);
        if (headerResponse is null)
            return null;

        IEnumerable<PurchaseDeliveryNoteLineSAPDTO> linesResponse = await receivingIntegration.GetGRPODraftLinesAsync(request.DocEntry);

        PurchaseDeliveryNoteDTO purchaseDeliveryNoteDTO = headerResponse.Adapt<PurchaseDeliveryNoteDTO>();
        IEnumerable<PurchaseDeliveryNoteLineDTO> purchaseDeliveryNoteLineDTO = linesResponse.Adapt<IEnumerable<PurchaseDeliveryNoteLineDTO>>();

        purchaseDeliveryNoteDTO.DocumentLines = [.. purchaseDeliveryNoteLineDTO];

        return purchaseDeliveryNoteDTO;
    }
}