namespace Application.DataTransferObjects.Transactions.SalesReturn.SAP;

public class SalesReturnLinesSAPDTO
{
    public int DocEntry {  get; set; }
    public int DocNum {  get; set; }
    public int LineNum {  get; set; }
    public string ItemCode {  get; set; }
    public string ItemName {  get; set; }
    public string WhsCode {  get; set; }
    public string WhsName {  get; set; }
    public decimal TargetQuantity {  get; set; }
    public decimal Quantity {  get; set; }
    public decimal OpenQuantity {  get; set; }
    public string UoMCode {  get; set; }
    public decimal UoMValue {  get; set; }
    public string UoMName {  get; set; }
    public string? ISBN {  get; set; }

    public int Freight1Code { get; set; }
    public decimal Freight1 { get; set; }
    public int Freight2Code { get; set; }
    public decimal Freight2 { get; set; }
    public decimal MarkUp { get; set; }

}
