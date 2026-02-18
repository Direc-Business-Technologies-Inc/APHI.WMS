namespace Integration.SAP.Entities.Transactional.Receiving;

public class PurchaseDeliveryNoteHeaderSAPDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public int BaseEntry { get; set; }
    public int BaseDocNum { get; set; }
    public DateTime DocDate { get; set; }
    public DateTime DocDueDate { get; set; }
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public string SupplierContactPerson { get; set; }
    public string ReceivedBy { get; set; }
}
