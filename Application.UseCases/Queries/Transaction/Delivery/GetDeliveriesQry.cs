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

public record GetDeliveriesQry(int[] docEntries) : IRequest<List<DeliveryDTO?>>;

public class GetDeliveriesQryHandler(
    IDeliveryIntegration deliveryIntegration)
    : IRequestHandler<GetDeliveriesQry, List<DeliveryDTO?>>
{
    const int PARRALEL_COUNT = 10;
    public async Task<List<DeliveryDTO?>> Handle(GetDeliveriesQry request, CancellationToken cancellationToken)
    {
        List<DeliveryDTO?> result = [];
        foreach (var chunk in request.docEntries.Chunk(PARRALEL_COUNT)) // i dont wanna do too many requests at once
        {
            var tasks = chunk.Select(entry => GetDelivery(entry));
            var results = await Task.WhenAll(tasks);
            result.AddRange(results);
        }
        return result;
    }


    private async Task<DeliveryDTO?> GetDelivery(int docEntry)
    {
        DeliveryHeaderSAPDTO? doc = await deliveryIntegration.GetDeliveryDocumentHeaderAsync(docEntry);

        if (doc is null)
            return null;

        IEnumerable<DeliveryLineSAPDTO> lines = await deliveryIntegration.GetDeliveryDocumentLinesAsync(docEntry);

        DeliveryDTO dto = doc.Adapt<DeliveryDTO>();
        dto.SapReference.DocEntry = doc.DocEntry;
        dto.SapReference.DocNum = doc.DocNum;
        dto.DocumentLines = lines.Adapt<IEnumerable<DeliveryLineDTO>>();
        return dto;
    }
}
