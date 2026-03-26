namespace Web.BlazorServer.ViewModels.Transaction.InventoryTransfer;

public class InventoryTransferCVULineVM
{
    public string ItemCode { get; set; }
    public string ItemDescription { get; set; }
    public string UoM { get; set; }
    public Decimal Quantity { get; set; }
}
