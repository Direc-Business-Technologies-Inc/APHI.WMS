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


public record GetDeliveryByCustomerDataGridQry(DataGridIntent Intent, string cardCode) : IRequest<(IEnumerable<DeliveryDataGridDTO> Data, int Count)>;

public class GetDeliveryByCustomerDataGridQryHandler(
    IDeliveryIntegration deliveryIntegration)
    : IRequestHandler<GetDeliveryByCustomerDataGridQry, (IEnumerable<DeliveryDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<DeliveryDataGridDTO> Data, int Count)> Handle(GetDeliveryByCustomerDataGridQry request, CancellationToken cancellationToken)
    {

        request.Intent.Filters.Add(new AppFilterDescriptor
        {
            FilterValueType = FilterValueTypeEnum.String,
            ComparisonOperator = ComparisonOperatorEnum.Equals,
            Value = request.cardCode,
            Property = "CardCode"
        });

        (IEnumerable<DeliveryDataGridSAPDTO> Data, int Count) = await deliveryIntegration.GetDeliveryDocumentsAsync(request.Intent);

        return (Data.Adapt<IEnumerable<DeliveryDataGridDTO>>(), Count);
    }
}
