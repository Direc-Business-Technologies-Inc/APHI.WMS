using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.GoodsReturn;

public class GoodsReturnRequestDTO : MarketingDocumentDTO
{
    public int GRPODocEntry { get; set; }
    public int GRPODocNum { get; set; }
    public int PODocEntry { get; set; }
    public int PODocNum { get; set; }
    public string PreparedBy { get; set; }


    public List<GRRLineDTO> DocumentLines { get; set; } = [];
}
