using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.GoodsReturn;

public class GoodsReturnDTO : MarketingDocumentDTO
{
    public int GRRDocEntry { get; set; }
    public int GRRDocNum { get; set; }
    public int GRPODocEntry { get; set; }
    public int GRPODocNum { get; set; }
    public int PODocEntry { get; set; }
    public int PODocNum { get; set; }
    public string PreparedBy { get; set; }

    public List<GoodsReturnLineDTO> DocumentLines { get; set; } = [];
}
