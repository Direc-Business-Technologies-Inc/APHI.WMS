using Application.DataTransferObjects.Transactions.GoodsReturn;
using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.GoodsReturn;

public class GoodsReturnVM : MarketingDocumentVM
{
    public int GRRDocEntry { get; set; }
    public int GRRDocNum { get; set; }
    public int GRPODocEntry { get; set; }
    public int GRPODocNum { get; set; }
    public int PODocEntry { get; set; }
    public int PODocNum { get; set; }
    public string PreparedBy { get; set; }
    public WarehouseVM Warehouse { get; set; } = new();

    public List<GoodsReturnLineVM> DocumentLines { get; set; } = [];

    public bool ValidateWarehouse() => !string.IsNullOrEmpty(Warehouse.WhsCode);
    public bool ValidateBusinessPartner() => !string.IsNullOrEmpty(BusinessPartner.CardCode);
}
