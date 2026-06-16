using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.GoodsReceipt;

public class GoodsReceiptLineDTO : ItemDTO
{
    public decimal OnHandQty { get; set; }
    public WarehouseDTO Warehouse { get; set; }
}
