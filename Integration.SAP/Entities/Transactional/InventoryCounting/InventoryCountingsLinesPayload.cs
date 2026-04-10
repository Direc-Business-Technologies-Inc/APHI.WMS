using Ardalis.GuardClauses;
using System.Text.Json.Serialization;

namespace Integration.SAP.Entities.Transactional.InventoryCounting;

public class InventoryCountingsLinesPayload
{
    public string ItemCode { get; private set; }
    public string WarehouseCode { get; private set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UoMCode { get; private set; }
    public decimal CountedQuantity { get; private set; }

    public InventoryCountingsLinesPayload(string itemCode, string whsCode, string? uomCode, decimal ctdQty)
    {
        ItemCode = Guard.Against.NullOrEmpty(itemCode, nameof(ItemCode), "Item Code cant be null");
        WarehouseCode = Guard.Against.NullOrEmpty(whsCode, nameof(WarehouseCode), "Warehouse Code cant be null");
        UoMCode = string.IsNullOrEmpty(uomCode) || uomCode == "Manual" ? null : uomCode;
        CountedQuantity = Guard.Against.Negative(ctdQty, nameof(CountedQuantity), "Counted Quantity cannot be negative");
    }
}
