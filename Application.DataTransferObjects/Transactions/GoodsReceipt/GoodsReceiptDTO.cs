using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.GoodsReceipt;

public class GoodsReceiptDTO : MarketingDocumentDTO
{
    public string PreparedBy { get; set; }
    public TransactionTypeDTO TransactionType { get; set; }
    public IEnumerable<GoodsReceiptLineDTO> DocumentLines { get; set; } = [];
}
