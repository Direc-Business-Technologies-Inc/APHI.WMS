using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;
using Web.BlazorServer.ViewModels.Transaction.GoodsIssue;

namespace Web.BlazorServer.ViewModels.Transaction.GoodsReceipt;

public class GoodsReceiptVM : MarketingDocumentVM
{
    public string PreparedBy { get; set; } = string.Empty;
    public WarehouseVM Warehouse { get; set; } = new();
    public TransactionTypeVM TransactionType { get; set; } = new();
    public IEnumerable<GoodsReceiptLineVM> DocumentLines { get; set; } = [];
}
