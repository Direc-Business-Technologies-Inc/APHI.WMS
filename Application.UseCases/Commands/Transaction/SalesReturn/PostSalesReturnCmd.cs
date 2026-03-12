using Application.DataTransferObjects.Transactions.SalesReturn;
using Application.UseCases.Repositories.Bases;
using MediatR;

namespace Application.UseCases.Commands.Transaction.SalesReturn;

public record PostSalesReturnCmd(SalesReturnDTO Data) : ITransactionalRequest<bool>;

public class PostSalesReturnCmdHandler() : IRequestHandler<PostSalesReturnCmd, bool>
{
    public Task<bool> Handle(PostSalesReturnCmd request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

