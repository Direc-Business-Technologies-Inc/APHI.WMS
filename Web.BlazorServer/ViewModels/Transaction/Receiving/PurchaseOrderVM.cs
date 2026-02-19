using Application.DataTransferObjects.Transactions.Receiving;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.Receiving;

public class PurchaseOrderVM : MarketingDocumentVM
{
    public string Remarks { get; set; } = string.Empty;
    public string SupplierContactPerson { get; set; } = string.Empty;
    public string ReceivedBy { get; set; } = string.Empty;
    public List<PurchaseOrderLineVM> DocumentLines { get; set; } = [];

    public bool Validate() => !string.IsNullOrEmpty(ReceivedBy) && !DocumentLines.All(dl => dl.Quantity > 0);
}
