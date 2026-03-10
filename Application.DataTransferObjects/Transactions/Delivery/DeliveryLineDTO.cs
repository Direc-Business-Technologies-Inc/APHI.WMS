using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Delivery;

internal class DeliveryLineDTO:ItemDTO
{
    public WarehouseDTO Warehouse { get; set; }

}
