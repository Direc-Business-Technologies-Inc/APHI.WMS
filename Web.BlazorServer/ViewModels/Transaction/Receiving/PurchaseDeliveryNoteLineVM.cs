using Domain.Entities.Enums.Transaction.Receiving;
using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.Receiving;

public class PurchaseDeliveryNoteLineVM : ItemVM
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public int BaseEntry { get; set; }
    public int BaseDocNum { get; set; }
    public int BaseLine { get; set; }
    public string TaxCode { get; set; }
    public WarehouseVM Warehouse { get; set; }
    public InputType InputType { get; set; }
}
