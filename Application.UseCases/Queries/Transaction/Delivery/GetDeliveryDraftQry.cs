using Application.DataTransferObjects.Transactions.Delivery;
using Application.DataTransferObjects.Transactions.Delivery.SAP;
using Application.UseCases.Repositories.Integration.Transaction.Delivery;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.Delivery;

public record GetDeliveryDraftQry(int DocEntry) : IRequest<DeliveryDTO?>;

public class GetDeliveryDraftQryHandler(
    IDeliveryIntegration integration
    ) : IRequestHandler<GetDeliveryDraftQry, DeliveryDTO?>
{
    public async Task<DeliveryDTO?> Handle(GetDeliveryDraftQry request, CancellationToken cancellationToken)
    {
        DeliveryHeaderSAPDTO? doc = await integration.GetDeliveryDraftDocumentHeaderAsync(request.DocEntry);

        if (doc is null)
            return null;

        IEnumerable<DeliveryLineSAPDTO> lines = await integration.GetDeliveryDocumentLinesAsync(request.DocEntry);

        DeliveryDTO dto = doc.Adapt<DeliveryDTO>();
        dto.DocumentLines = lines.Adapt<IEnumerable<DeliveryLineDTO>>();

        return dto;
    }
}
