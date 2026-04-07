using Application.DataTransferObjects.Transactions.InventoryCounting;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Transaction.InventoryCounting;
using Domain.Entities.ValueObjects.Others;
using Domain.Entities.ValueObjects.Transaction;
using Domain.ValueObjects.Transaction;
using MediatR;

namespace Application.UseCases.Commands.Transaction.InventoryCounting;

public record CreateInventoryCountingDocumentCmd(InventoryCountingDocumentDTO Data) : ITransactionalRequest<bool>;

public class CreateInventoryCountingDocumentCmdHandler(
    IAppCommandRepository appCommandRepo) 
    : IRequestHandler<CreateInventoryCountingDocumentCmd, bool>
{
    public async Task<bool> Handle(CreateInventoryCountingDocumentCmd request, CancellationToken cancellationToken)
    {
        var data = request.Data;

        var documentLines = data.DocumentLines.Select(line => new InventoryCountingDocumentLineVO(
            line.ItemCode,
            line.ItemName,
            0, // Initial actual quantity
            line.Quantity,
            line.UoMCode,
            line.UoMValue,
            line.UoMName
        )).ToList();

        SapDocumentReferenceVO? sapRef = data.SapReference.DocNum == 0 
            ? null 
            : new SapDocumentReferenceVO(data.SapReference.DocEntry, data.SapReference.DocNum);

        var dem = new InventoryCountingDocumentDEM(
            data.DocumentType.Id,
            new AppDocNumVO(data.LsmsDocNum.Value),
            new WarehouseVO(data.Warehouse.WhsCode, data.Warehouse.WhsName),
            data.CountingDate,
            data.CycleType,
            documentLines,
            data.Remarks,
            sapRef
        );

        await appCommandRepo.AddAsync(dem);
        return true;
    }
}
