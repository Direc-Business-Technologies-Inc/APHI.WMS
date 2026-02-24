using Application.DataTransferObjects.Transactions.GoodsReturn;
using Application.UseCases.Repositories.Integration.Transaction.GoodsReturn;
using Mapster;
using MediatR;

namespace Application.UseCases.Commands.Transaction.GoodsReturn;

public record PostGoodsReturnCmd(GoodsReturnRequestDTO Data) : IRequest<bool>;

public class PostGoodsReturnCmdHandler(
    IGoodsReturnIntegration goodsReturnIntegration) 
    : IRequestHandler<PostGoodsReturnCmd, bool>
{
    public async Task<bool> Handle(PostGoodsReturnCmd request, CancellationToken cancellationToken)
    {
        GoodsReturnDTO payload = request.Data.Adapt<GoodsReturnDTO>();

        bool result = await goodsReturnIntegration.PostGoodsReturnAsync(payload);

        return result;
    }
}
