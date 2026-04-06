using Ardalis.GuardClauses;
using Domain.ValueObjects;

namespace Domain.Entities.ValueObjects.Transaction;

public class InventoryCountingSheetLineVO : ValueObject
{
    public string SheetNo { get; private set; }
    public string ItemCode { get; private set; }
    public string ItemName { get; private set; }
    public decimal Quantity { get; private set; }
    public string UoMCode { get; private set; }
    public decimal UoMValue { get; private set; }
    public string UoMName { get; private set; }

    public InventoryCountingSheetLineVO() { }

    public InventoryCountingSheetLineVO(string sheetNo,
                                        string itemCode,
                                        string itemName,
                                        decimal quantity,
                                        string uomCode,
                                        decimal uomValue,
                                        string uomName)
    {
        SheetNo = Guard.Against.NullOrEmpty(sheetNo, nameof(SheetNo), "Sheet No cannot be null or empty");
        ItemCode = Guard.Against.NullOrEmpty(itemCode, nameof(ItemCode), "Item Code cannot be null or empty");
        ItemName = Guard.Against.NullOrEmpty(itemName, nameof(ItemName), "Item Name cannot be null or empty");
        Quantity = Guard.Against.Null(quantity, nameof(Quantity), "Item Quantity cannot be null");
        UoMCode = Guard.Against.NullOrEmpty(uomCode, nameof(UoMCode), "UoM Code cannot be null or empty");
        UoMValue = Guard.Against.Null(uomValue, nameof(UoMValue), "UoM Value cannot be null");
        UoMValue = Guard.Against.NegativeOrZero(uomValue, nameof(UoMValue), "UoM Value cannot be negative or zero");
        UoMName = Guard.Against.NullOrEmpty(uomName, nameof(UoMName), "UoM Name cannot be null or empty");
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
