using Ardalis.GuardClauses;
using Domain.Entities.Entities.Transaction.Common;

namespace Domain.Entities.Entities.Transaction.InventoryCounting;

public class InventoryCountingDocumentLineDEM : ItemDEM
{
    public decimal InStockQuantity { get; private set; }
    public Guid InventoryCountingDocumentId { get; private set; }

    public InventoryCountingDocumentLineDEM() { }

    public InventoryCountingDocumentLineDEM(
        Guid inventoryCountingDocumentId,
        string itemCode,
        string itemName,
        decimal inStockQuantity,
        string uomCode,
        decimal uomValue,
        string uomName) : base(itemCode, itemName, inStockQuantity, uomCode, uomValue, uomName)
    {
        InventoryCountingDocumentId = Guard.Against.NullOrEmpty(inventoryCountingDocumentId, nameof(InventoryCountingDocumentId), "Document Id cannot be empty");
        InStockQuantity = Guard.Against.Negative(inStockQuantity, nameof(InStockQuantity), "In Stock Quantity cannot be negative");
    }

    public InventoryCountingDocumentLineDEM UpdateInStockQuantity(decimal inStockQuantity)
    {
        InStockQuantity = Guard.Against.Negative(inStockQuantity, nameof(InStockQuantity), "In Stock Quantity cannot be negative");
        return this;
    }
}
