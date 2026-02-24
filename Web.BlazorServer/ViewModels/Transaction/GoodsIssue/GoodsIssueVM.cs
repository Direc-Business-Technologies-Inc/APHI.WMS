using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.GoodsIssue;

public class GoodsIssueVM : MarketingDocumentVM
{
    public string PreparedBy { get; set; } = string.Empty;
    public TransactionTypeVM TransactionType { get; set; } = new();
    public WarehouseVM Warehouse { get; set; } = new();
    public IEnumerable<GoodsIssueLineVM> DocumentLines { get; set; } = [];
}
