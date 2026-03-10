namespace Application.DataTransferObjects.Transactions.Delivery.SAP;

public class SalesOrderHeaderSAPDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public DateTime DocDate { get; set; }
    public DateTime DocDueDate { get; set; }
    public DateTime TaxDate { get; set; }
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public string CntctCode { get; set; }
    public string NumAtCard { get; set; }

    public string SchlYear { get; set; }
    public string PONo { get; set; }
    public string Area { get; set; }
    public string Desig { get; set; }
    public string OrdBy { get; set; }
    public string Remarks { get; set; }
    public string DocRemarks { get; set; }
    public string PrepBy { get; set; }
    public string RevBy { get; set; }
    public string AppBy { get; set; }
    public string NotedBy { get; set; }



}
