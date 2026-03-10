namespace Application.DataTransferObjects.Transactions.Delivery.SAP;

public class SalesOrderHeaderSAPDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public DateTime DocDate { get; set; }
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public string ContactPerson { get; set; }
    public string DocRemarks { get; set; }
    public string PrepBy { get; set; }
    public string RevBy { get; set; }
}
