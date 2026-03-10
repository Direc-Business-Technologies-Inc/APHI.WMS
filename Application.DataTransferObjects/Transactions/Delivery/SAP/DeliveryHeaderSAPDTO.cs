namespace Application.DataTransferObjects.Transactions.Delivery.SAP;

internal class DeliveryHeaderSAPDTO
{

    public string CardCode { get; set; }
    public string CardName { get; set; }
    public string CntctCode { get; set; }
    public int NumAtCard { get; set; }

    public int DocNum { get; set; }
    public int DocEntry { get; set; }
    public DateTime DocDate { get; set; }
    public DateTime DocDueDate { get; set; }
    public DateTime TaxDate { get; set; }

    public string ItemCode { get; set; }
    public string Dscription { get; set; }
    public int Quantity { get; set; }
    public string UoMCode { get; set; }
    public string WhsCode { get; set; }
    public string WhsName { get; set; }

    public string SchlYear { get; set; }
    public string DRNo { get; set; }
    public string ActualDelivDate { get; set; }
    public string DelivMeans { get; set; }
    public string Courier { get; set; }
    public string CourName { get; set; }
    public string Desig { get; set; }
    public string WBNo { get; set; }
    public string PlateNo { get; set; }
    public string Driver { get; set; }
    public string Remarks { get; set; }
    public string RecBy { get; set; }
    public string PrepBy { get; set; }
    public string AppBy { get; set; }
    public string NotedBy { get; set; }



}
