using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.Receiving;

public class PurchaseDeliveryNoteVM : MarketingDocumentVM
{
    public string SupplierContactPerson { get; set; }
    public string ReceivedBy { get; set; }
    public List<PurchaseDeliveryNoteLineVM> DocumentLines { get; set; } = [];
}
