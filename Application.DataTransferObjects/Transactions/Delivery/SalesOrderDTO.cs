namespace Application.DataTransferObjects.Transactions.Delivery;

internal class SalesOrderDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
    public DateTime DocDate { get; set; }
    public DateTime DocDueDate { get; set; }
    public DateTime TaxDate { get; set; }
    public string? Customer { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? ContactPerson { get; set; } = null;
    public string? DRNo { get; set; } = null;

    public string? SchoolYear { get; set; } = null;
    public string? PONo { get; set; } = null;
    public string? Area { get; set; } = null;
    public string? Designation { get; set; } = null;
    public string? OrderedBy { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public string? DocRemarks { get; set; } = null;
    public string? PreparedBy { get; set; } = null;
    public string? ReviewedBy { get; set; } = null;
    public string? ApprovedBy { get; set; } = null;
    public string? NotedBy { get; set; }
    public IEnumerable<SalesOrderDTO> DocumentLine{ get; set; } = [];

}
