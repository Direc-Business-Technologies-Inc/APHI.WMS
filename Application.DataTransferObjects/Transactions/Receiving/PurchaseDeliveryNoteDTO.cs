using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.Receiving;

public class PurchaseDeliveryNoteDTO : MarketingDocumentDTO
{
    public string SupplierContactPerson { get; set; }
    public string ReceivedBy { get; set; }
    public List<PurchaseDeliveryNoteLineDTO> DocumentLines { get; set; } = [];
}
