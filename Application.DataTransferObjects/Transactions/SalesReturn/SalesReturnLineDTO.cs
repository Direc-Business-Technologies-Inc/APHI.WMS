using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.SalesReturn;

public class SalesReturnLineDTO : ItemDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public int BaseEntry { get; set; }
    public int BaseDocNum { get; set; }
    public int BaseLine { get; set; }
    public decimal TargetQuantity { get; set; }
    public decimal OpenQuantity { get; set; }
    public WarehouseDTO? Warehouse { get; set; }

    public int Freight1Code { get; set; }
    public decimal Freight1 { get; set; }
    public int Freight2Code { get; set; }
    public decimal Freight2 { get; set; }
    public decimal MarkUp { get; set; }
}
