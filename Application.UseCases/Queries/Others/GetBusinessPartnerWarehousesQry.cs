using Application.DataTransferObjects.Others;
using Application.UseCases.Repositories.Integration.Others;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Others;

public record GetBusinessPartnerWarehousesQry(string cardCode) : IRequest<IEnumerable<WarehouseDTO>>;

public class GetBusinessPartnerWarehousesQryHandler(
    IBusinessPartnerIntegration bpIntegration)
    : IRequestHandler<GetBusinessPartnerWarehousesQry, IEnumerable<WarehouseDTO>>
{
    public Task<IEnumerable<WarehouseDTO>> Handle(GetBusinessPartnerWarehousesQry request, CancellationToken cancellationToken)
    {
        return bpIntegration.GetBusinessPartnerWarehouses(request.cardCode);
    }
}
