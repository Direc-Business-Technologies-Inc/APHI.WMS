namespace Application.DataTransferObjects.Transactions.Delivery;

public class DeliveryDataGridDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public DateTime DocDate { get; set; }
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public string ContactPerson { get; set; }
    public string Remarks { get; set; }
    public string DocStatus { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
}
