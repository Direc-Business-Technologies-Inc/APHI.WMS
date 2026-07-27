using Application.DataTransferObjects.Transactions.SalesReturn;
using Application.UseCases.Repositories.Integration.Transaction.SalesReturn;
using Mapster;
using MediatR;
using Shared.Libraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.SalesReturn;

public record GetSalesReturnDraftsDataGridQry(DataGridIntent Intent) : IRequest<(IEnumerable<SalesReturnDataGridDTO> Data, int Count)>;

public class GetSalesReturnDraftsDataGridQryHandler(
    ISalesReturnIntegration integration
    ) : IRequestHandler<GetSalesReturnDraftsDataGridQry, (IEnumerable<SalesReturnDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<SalesReturnDataGridDTO> Data, int Count)> Handle(GetSalesReturnDraftsDataGridQry request, CancellationToken cancellationToken)
    {
        (var Data, int Count) = await integration.GetSalesReturnDraftDataAsync(request.Intent);
        return (Data.Adapt<IEnumerable<SalesReturnDataGridDTO>>(), Count);
    }
}
