using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.GoodsIssue;

public class GoodsIssueLineDTO : ItemDTO
{
    public decimal OnHandQty { get; set; }
    public WarehouseDTO Warehouse { get; set; }
}
