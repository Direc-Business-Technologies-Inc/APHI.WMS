using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Delivery;

public class DeliveryLineDTO:ItemDTO
{
    public WarehouseDTO Warehouse { get; set; }
    public int? CreatedFrom { get; set; }
    public int Freight1Code { get; set; }
    public decimal Freight1 { get; set; }
    public int Freight2Code { get; set; }
    public decimal Freight2 { get; set; }
}
