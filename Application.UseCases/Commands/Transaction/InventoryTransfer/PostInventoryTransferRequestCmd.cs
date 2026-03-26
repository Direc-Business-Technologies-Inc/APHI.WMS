using Application.DataTransferObjects.Transactions.InventoryTransfer;
using Application.UseCases.Repositories.Integration.Transaction.InventoryTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.UseCases.Commands.Transaction.InventoryTransfer;

public record PostInventoryTransferRequestCmd(InventoryTransferRequestDTO Data) : IRequest<bool>;

public class PostInventoryTransferRequestCmdHandler(
    IInventoryTransferIntegration inventoryTransferIntegration
) : IRequestHandler<PostInventoryTransferRequestCmd, bool>
{
    public async Task<bool> Handle(PostInventoryTransferRequestCmd request, CancellationToken cancellationToken)
    {
        return await inventoryTransferIntegration.PostInventoryTransferRequest(request.Data);
    }
}