using Ardalis.GuardClauses;
using Domain.Entities.Enums.Transaction.InventoryCounting;
using Domain.Entities.Transaction.Common;
using Domain.ValueObjects.Transaction;

namespace Domain.Entities.Entities.Transaction.InventoryCounting;

public class InventoryCountingDocumentDEM : TransactionalDocumentDEM
{
    public string WarehouseCode { get; private set; }
    public string WarehouseName { get; private set; }
    public DateTime CountingDate { get; private set; }
    public CycleType CycleType { get; private set; }
    public InventoryCountingDocumentStatus Status { get; private set; }
    public string? Remarks { get; private set; }

    public List<InventoryCountingDocumentLineDEM> DocumentLines { get; private set; } = [];
    public List<InventoryCountingSheetDEM> Sheets { get; private set; } = [];

    public InventoryCountingDocumentDEM() { }

    public InventoryCountingDocumentDEM(
        Guid documentTypeId,
        AppDocNumVO lsmsDocNum,
        string warehouseCode,
        string warehouseName,
        DateTime countingDate,
        CycleType cycleType,
        string? remarks = null,
        SapDocumentReferenceVO? sapReference = null) : base(documentTypeId, lsmsDocNum, sapReference)
    {
        WarehouseCode = Guard.Against.NullOrEmpty(warehouseCode, nameof(WarehouseCode), "Warehouse Code cannot be null or empty");
        WarehouseName = Guard.Against.NullOrEmpty(warehouseName, nameof(WarehouseName), "Warehouse Name cannot be null or empty");
        CountingDate = Guard.Against.NullOrOutOfSQLDateRange(countingDate, nameof(CountingDate), "Counting Date cannot be null or out of range");
        CycleType = cycleType;
        Status = InventoryCountingDocumentStatus.Open;
        Remarks = remarks;
    }

    public InventoryCountingDocumentDEM UpdateStatus(InventoryCountingDocumentStatus status)
    {
        Status = status;
        return this;
    }

    public InventoryCountingDocumentDEM UpdateRemarks(string? remarks)
    {
        Remarks = remarks;
        return this;
    }
}
