using Application.DataTransferObjects.Transactions.Delivery;
using Application.DataTransferObjects.Transactions.Delivery.SAP;
using Application.UseCases.Repositories.Integration.Transaction.Delivery;
using Mapster;
using MediatR;
using Shared.Libraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.Delivery;

public record GetDeliveryDraftsDatagridQry(DataGridIntent Intent) : IRequest<(IEnumerable<DeliveryDataGridDTO> Data, int Count)>;

public class GetDeliveryDraftsDatagridQryHandler(
    IDeliveryIntegration integration) : IRequestHandler<GetDeliveryDraftsDatagridQry, (IEnumerable<DeliveryDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<DeliveryDataGridDTO> Data, int Count)> Handle(GetDeliveryDraftsDatagridQry request, CancellationToken cancellationToken)
    {
        (IEnumerable<DeliveryDataGridSAPDTO> Data, int Count) = await integration.GetDeliveryDraftDocumentsAsync(request.Intent);

        return (Data.Adapt<IEnumerable<DeliveryDataGridDTO>>(), Count);
    }
}