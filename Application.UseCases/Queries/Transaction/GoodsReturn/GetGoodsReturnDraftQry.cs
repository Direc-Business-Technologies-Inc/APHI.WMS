using Application.DataTransferObjects.Transactions.GoodsReturn;
using Application.DataTransferObjects.Transactions.GoodsReturn.SAP;
using Application.UseCases.Repositories.Integration.Transaction.GoodsReturn;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Transaction.GoodsReturn;

public record GetGoodsReturnDraftQry(int DocEntry) : IRequest<GoodsReturnDTO?>;

public class GetGoodsReturnDraftQryHandler(
    IGoodsReturnIntegration goodsReturnIntegration)
    : IRequestHandler<GetGoodsReturnDraftQry, GoodsReturnDTO?>
{
    public async Task<GoodsReturnDTO?> Handle(GetGoodsReturnDraftQry request, CancellationToken cancellationToken)
    {
        GoodsReturnHeaderSAPDTO? headerResponse = await goodsReturnIntegration.GetGoodsReturnDraftHeaderAsync(request.DocEntry);

        if (headerResponse is null)
            return null;

        IEnumerable<GoodsReturnLineSAPDTO> linesResponse = await goodsReturnIntegration.GetGoodsReturnDraftLinesAsync(request.DocEntry);

        GoodsReturnDTO data = headerResponse.Adapt<GoodsReturnDTO>();
        IEnumerable<GoodsReturnLineDTO> lineData = linesResponse.Adapt<IEnumerable<GoodsReturnLineDTO>>();

        data.DocumentLines = [.. lineData];

        return data;

    }
}