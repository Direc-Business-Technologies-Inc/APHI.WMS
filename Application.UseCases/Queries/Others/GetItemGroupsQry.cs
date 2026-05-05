using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Mapster;
using MediatR;
using Shared.Entities;

namespace Application.UseCases.Queries.Others;

public record GetItemGroupsQry(DataGridIntent Intent) : IRequest<(IEnumerable<ItemGroupDTO> Data, int Count)>;

public class GetItemGroupsQryHandler(
    IItemGroupsIntegration integ)
    : IRequestHandler<GetItemGroupsQry, (IEnumerable<ItemGroupDTO> Data, int Count)>
{
    public async Task<(IEnumerable<ItemGroupDTO> Data, int Count)> Handle(GetItemGroupsQry request, CancellationToken cancellationToken)
    {
        (IEnumerable<ItemGroupSAPDTO> Data, int Count) = await integ.GetItemGroupsAsync(request.Intent);

        return (Data.Adapt<IEnumerable<ItemGroupDTO>>(), Count);
    }
}
