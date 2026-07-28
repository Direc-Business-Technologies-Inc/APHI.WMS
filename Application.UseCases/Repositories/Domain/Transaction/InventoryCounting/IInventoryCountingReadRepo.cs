using Application.DataTransferObjects.Transactions.InventoryCounting;
using Domain.Entities.Enums.Transaction.InventoryCounting;
using Shared.Libraries.Entities;

namespace Application.UseCases.Repositories.Domain.Transaction.InventoryCounting;

public interface IInventoryCountingReadRepo
{
    Task<(IEnumerable<InventoryCountingDataGridDTO> data, int count)> GetInventoryCountingDataGrid(DataGridIntent intent);
    Task<InventoryCountingDocumentDTO?> GetInventoryCountingDocument(Guid id);
    Task<IEnumerable<string>> ExistsDocumentForWarehouseAndCycleInPeriodAsync(string whsCode, string[] itemCodes, CycleType cycleType, DateTime countingDate);
}
