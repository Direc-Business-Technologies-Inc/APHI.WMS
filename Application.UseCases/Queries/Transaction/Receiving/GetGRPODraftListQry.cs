using Application.DataTransferObjects.Transactions.Receiving;
using Application.UseCases.Repositories.Integration.Transaction.Receiving;
using Integration.SAP.Entities.Transactional.Receiving;
using Mapster;
using MediatR;
using Shared.Libraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.Receiving;

public record GetGRPODraftListQry(DataGridIntent Intent) : IRequest<(IEnumerable<PurchaseDeliveryNoteDataGridDTO> Data, int Count)>;

public class GetGRPODraftListQryHandler(
    IReceivingIntegration receivingIntegration)
    : IRequestHandler<GetGRPODraftListQry, (IEnumerable<PurchaseDeliveryNoteDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<PurchaseDeliveryNoteDataGridDTO> Data, int Count)> Handle(GetGRPODraftListQry request, CancellationToken cancellationToken)
    {
        (IEnumerable<PurchaseDeliveryNoteSAPDTO> Data, int Count) = await receivingIntegration.GetGRPODraftListAsync(request.Intent);

        return (Data.Adapt<IEnumerable<PurchaseDeliveryNoteDataGridDTO>>(), Count);
    }
}