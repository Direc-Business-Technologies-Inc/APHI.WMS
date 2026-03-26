
namespace Integration.SAP.Entities.Transactional.InventoryTransfer;

public class InventoryTransferLineSAPDTO
{
    public string ItemCode { get; set; }
    public string ItemDescription { get; set; }
    public string UoM { get; set; }
    public Decimal Quantity { get; set; }
}
