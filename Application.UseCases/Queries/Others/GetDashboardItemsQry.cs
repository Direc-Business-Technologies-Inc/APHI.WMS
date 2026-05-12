using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Mapster;
using MediatR;
using Shared.Libraries.Entities;

namespace Application.UseCases.Queries.Others;

public record GetDashboardItemsQry() : IRequest<DashboardItemsDTO>;

public class GetDashboardItemsQryHandler(
    IDashboardItemsIntegration integration)
    : IRequestHandler<GetDashboardItemsQry, DashboardItemsDTO>
{
    public async Task<DashboardItemsDTO> Handle(GetDashboardItemsQry request, CancellationToken cancellationToken)
    {
        var data = await integration.GetDashboardItemsAsync();

        return data.Adapt<DashboardItemsDTO>();
    }
}
