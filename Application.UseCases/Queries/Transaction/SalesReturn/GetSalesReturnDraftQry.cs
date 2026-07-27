using Application.DataTransferObjects.Transactions.SalesReturn;
using Application.DataTransferObjects.Transactions.SalesReturn.SAP;
using Application.UseCases.Repositories.Integration.Transaction.SalesReturn;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.SalesReturn;

public record GetSalesReturnDraftQry(int DocEntry) : IRequest<SalesReturnDTO?>;

public class GetSalesReturnDraftQryHandler(
    ISalesReturnIntegration integration) : IRequestHandler<GetSalesReturnDraftQry, SalesReturnDTO?>
{
    public async Task<SalesReturnDTO?> Handle(GetSalesReturnDraftQry request, CancellationToken cancellationToken)
    {
        SalesReturnHeaderSAPDTO? header = await integration.GetSalesReturnDraftHeaderAsync(request.DocEntry);

        if (header is null)
            return null;

        IEnumerable<SalesReturnLinesSAPDTO> lines = await integration.GetSalesReturnDraftLinesAsync(request.DocEntry);

        SalesReturnDTO dto = header.Adapt<SalesReturnDTO>();
        dto.DocumentLines = lines.Adapt<List<SalesReturnLineDTO>>();

        return dto;
    }
}