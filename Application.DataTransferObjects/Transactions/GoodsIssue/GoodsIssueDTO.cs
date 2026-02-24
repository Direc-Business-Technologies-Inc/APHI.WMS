using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;
using Application.DataTransferObjects.Transactions.GoodsIssue;

namespace Application.DataTransferObjects.Transactions.Goodsissue;

public class GoodsIssueDTO : MarketingDocumentDTO
{
    public string PreparedBy { get; set; }
    public TransactionTypeDTO TransactionType { get; set; }
    public IEnumerable<GoodsIssueLineDTO> DocumentLines { get; set; } = [];
}
