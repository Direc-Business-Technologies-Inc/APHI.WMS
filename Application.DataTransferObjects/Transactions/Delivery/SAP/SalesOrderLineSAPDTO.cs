using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Delivery.SAP;

public class SalesOrderLineSAPDTO : ItemDTO
{
    public string WhsCode { get; set; }
    public string WhsName { get; set; }
    public string TargetQty { get; set; }
    public string OpenQty { get; set; }

}
