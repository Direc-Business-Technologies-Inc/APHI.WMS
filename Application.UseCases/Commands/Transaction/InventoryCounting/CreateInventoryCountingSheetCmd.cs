using Application.DataTransferObjects.Transactions.InventoryCounting;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.Transaction.InventoryCounting;
using Domain.Entities.ValueObjects.Transaction;
using Domain.ValueObjects.Transaction;
using MediatR;

namespace Application.UseCases.Commands.Transaction.InventoryCounting;

public record CreateInventoryCountingSheetCmd(InventoryCountingSheetDTO Data) : ITransactionalRequest<bool>;

public class CreateInventoryCountingSheetCmdHandler(
    IAppCommandRepository appCommandRepo,
    IAppReadRepository appReadRepo) 
    : IRequestHandler<CreateInventoryCountingSheetCmd, bool>
{
    public async Task<bool> Handle(CreateInventoryCountingSheetCmd request, CancellationToken cancellationToken)
    {
        var dem = await appReadRepo.FirstOrDefaultAsync<InventoryCountingDocumentDEM>(d => d.Id == request.Data.InventoryCountingDocumentId);
        
        if (dem == null)
            throw new Exception("Inventory Counting Document not found.");

        var sheetLines = request.Data.SheetLines.Select(sl => new InventoryCountingSheetLineVO(
            request.Data.SheetNo.Value,
            sl.ItemCode,
            sl.ItemName,
            sl.Quantity,
            sl.UoMCode,
            sl.UoMValue,
            sl.UoMName
        )).ToList();

        var sheet = new InventoryCountingSheetVO(
            new AppDocNumVO(request.Data.SheetNo.Value),
            request.Data.InventoryCountingDocumentId,
            request.Data.Counter.UserId,
            request.Data.SubmittedDate,
            sheetLines
        );

        dem.AddSheet(sheet);

        appCommandRepo.Update(dem);
        
        return true;
    }
}
