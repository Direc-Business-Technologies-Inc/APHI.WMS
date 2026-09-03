using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.SalesReturn;

public class SalesReturnLineVM : ItemVM
{
    public string? DRNo { get; set; }
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public int BaseEntry { get; set; }
    public int BaseDocNum { get; set; }
    public int BaseLine { get; set; }
    public decimal TargetQuantity { get; set; }
    public decimal OpenQuantity { get; set; }
    public WarehouseVM? Warehouse { get; set; } = null;

    public int Freight1Code { get; set; }
    public decimal Freight1 { get; set; }
    public int Freight2Code { get; set; }
    public decimal Freight2 { get; set; }
}
