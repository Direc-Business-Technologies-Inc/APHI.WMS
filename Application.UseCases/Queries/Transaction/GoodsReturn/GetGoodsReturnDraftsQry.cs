using Application.DataTransferObjects.Transactions.GoodsReturn;
using Application.DataTransferObjects.Transactions.GoodsReturn.SAP;
using Application.UseCases.Repositories.Integration.Transaction.GoodsReturn;
using Mapster;
using MediatR;
using Shared.Libraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.GoodsReturn;

public record GetGoodsReturnDraftsQry(DataGridIntent Intent) : IRequest<(IEnumerable<GoodsReturnDataGridDTO> Data, int Count)>;

public class GetGoodsReturnDraftsQryHandler(
    IGoodsReturnIntegration goodsReturnIntegration)
    : IRequestHandler<GetGoodsReturnDraftsQry, (IEnumerable<GoodsReturnDataGridDTO> Data, int Count)>
{
    public async Task<(IEnumerable<GoodsReturnDataGridDTO> Data, int Count)> Handle(GetGoodsReturnDraftsQry request, CancellationToken cancellationToken)
    {
        (IEnumerable<GoodsReturnsSAPDTO> Data, int Count) = await goodsReturnIntegration.GetGoodsReturnDraftsListAsync(request.Intent);

        return (Data.Adapt<IEnumerable<GoodsReturnDataGridDTO>>(), Count);
    }
}
