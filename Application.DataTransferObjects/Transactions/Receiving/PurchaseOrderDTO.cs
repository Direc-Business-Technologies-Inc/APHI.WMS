using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Receiving;

public class PurchaseOrderDTO : MarketingDocumentDTO
{
    public string Remarks { get; set; }
    public string SupplierContactPerson { get; set; }
    public string ReceivedBy { get; set; }
    public List<PurchaseOrderLineDTO> DocumentLines { get; set; } = [];
}
