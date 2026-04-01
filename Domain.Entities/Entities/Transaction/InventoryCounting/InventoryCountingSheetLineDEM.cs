using Ardalis.GuardClauses;
using Domain.Entities.Entities.Transaction.Common;

namespace Domain.Entities.Entities.Transaction.InventoryCounting;

public class InventoryCountingSheetLineDEM : ItemDEM
{
    public Guid InventoryCountingSheetId { get; private set; }

    public InventoryCountingSheetLineDEM() { }

    public InventoryCountingSheetLineDEM(
        Guid inventoryCountingSheetId,
        string itemCode,
        string itemName,
        decimal countedQuantity,
        string uomCode,
        decimal uomValue,
        string uomName) : base(itemCode, itemName, countedQuantity, uomCode, uomValue, uomName)
    {
        InventoryCountingSheetId = Guard.Against.NullOrEmpty(inventoryCountingSheetId, nameof(InventoryCountingSheetId), "Sheet Id cannot be empty");
    }
}
