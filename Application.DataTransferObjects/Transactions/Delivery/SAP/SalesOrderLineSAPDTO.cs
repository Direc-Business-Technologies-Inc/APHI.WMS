using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Delivery.SAP;

public class SalesOrderLineSAPDTO : ItemDTO
{
    public string WhsCode { get; set; }
    public string WhsName { get; set; }
    public decimal TargetQty { get; set; }
    public decimal OpenQty { get; set; }
    public decimal OnHand { get; set; }
    public int Freight1Code { get; set; }
    public decimal Freight1 { get; set; }
    public int Freight2Code { get; set; }
    public decimal Freight2 { get; set; }
    public decimal MarkUp { get; set; }
}
