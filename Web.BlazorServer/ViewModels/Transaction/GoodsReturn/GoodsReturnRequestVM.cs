using Application.DataTransferObjects.Transactions.GoodsReturn;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.GoodsReturn;

public class GoodsReturnRequestVM : MarketingDocumentVM
{
    public int GRPODocEntry { get; set; }
    public int GRPODocNum { get; set; }
    public int PODocEntry { get; set; }
    public int PODocNum { get; set; }
    public string PreparedBy { get; set; }


    public List<GRRLineVM> DocumentLines { get; set; } = [];
}
