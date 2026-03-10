using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Delivery;

public class SalesOrderLineDTO:ItemDTO
{
    public WarehouseDTO Warehouse { get; set; }

}
