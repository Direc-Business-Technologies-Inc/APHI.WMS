using Application.DataTransferObjects.Transactions.InventoryCounting;
using Shared.Entities;

namespace Application.UseCases.Repositories.Domain.Transaction.InventoryCounting;

public interface IInventoryCountingReadRepo
{
    Task<(IEnumerable<InventoryCountingDataGridDTO> data, int count)> GetInventoryCountingDataGrid(DataGridIntent intent);
    Task<InventoryCountingDocumentDTO?> GetInventoryCountingDocument(Guid id);
}
