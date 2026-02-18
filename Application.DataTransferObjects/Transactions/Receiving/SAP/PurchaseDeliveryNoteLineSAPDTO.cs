namespace Integration.SAP.Entities.Transactional.Receiving;

public class PurchaseDeliveryNoteLineSAPDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public string LineNum { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public decimal Quantity { get; set; }
    public string UoMCode { get; set; }
    public decimal UoMValue { get; set; }
    public string UoMName { get; set; }
    public string InputType { get; set; }
}
