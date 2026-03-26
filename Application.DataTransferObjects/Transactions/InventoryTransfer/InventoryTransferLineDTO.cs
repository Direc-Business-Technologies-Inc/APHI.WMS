namespace Application.DataTransferObjects.Transactions.InventoryTransfer;

public class InventoryTransferLineDTO
{
    public string ItemCode { get; set; }
    public string ItemDescription { get; set; }
    public string UoM { get; set; }
    public Decimal Quantity{ get; set; }
}
