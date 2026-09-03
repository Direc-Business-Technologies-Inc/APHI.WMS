using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.Delivery;

public class SalesOrderLineVM : ItemVM
{
    public WarehouseVM? Warehouse { get; set; }
    public decimal TargetQty { get; set; }
    public decimal OpenQty { get; set; }
    public decimal OnHand { get; set; }
    public int Freight1Code { get; set; }
    public decimal Freight1 { get; set; }
    public int Freight2Code { get; set; }
    public decimal Freight2 { get; set; }
}
