using Ardalis.GuardClauses;
using Domain.Commons;
using Domain.Entities.Enums.Transaction.InventoryCounting;
using Domain.Providers;

namespace Domain.Entities.Entities.Transaction.InventoryCounting;

public class InventoryCountingSheetDEM : AuditableDEM
{
    public string CounterName { get; private set; }
    public DateTime? SubmittedAt { get; private set; }
    public InventoryCountingSheetStatus Status { get; private set; }
    public Guid InventoryCountingDocumentId { get; private set; }

    public List<InventoryCountingSheetLineDEM> SheetLines { get; private set; } = [];

    public InventoryCountingSheetDEM() { }

    public InventoryCountingSheetDEM(
        Guid inventoryCountingDocumentId,
        string counterName)
    {
        InventoryCountingDocumentId = Guard.Against.NullOrEmpty(inventoryCountingDocumentId, nameof(InventoryCountingDocumentId), "Document Id cannot be empty");
        CounterName = Guard.Against.NullOrEmpty(counterName, nameof(CounterName), "Counter Name cannot be null or empty");
        Status = InventoryCountingSheetStatus.Async;
    }

    public InventoryCountingSheetDEM Submit()
    {
        SubmittedAt = DateTimeProvider.UtcNow;
        return this;
    }

    public InventoryCountingSheetDEM UpdateStatus(InventoryCountingSheetStatus status)
    {
        Status = status;
        return this;
    }
}
