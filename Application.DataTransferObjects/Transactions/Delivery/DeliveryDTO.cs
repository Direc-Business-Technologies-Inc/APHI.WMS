using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Transactions.GoodsReceipt;

namespace Application.DataTransferObjects.Transactions.Delivery;

internal class DeliveryDTO
{
   
    public string? DocRemarks { get; set; } = null;
    public string? Designation { get; set; } = null;
    public string? ReceivedBy { get; set; } = null;
    public string? ApprovedBy { get; set; } = null;
    public string? NotedBy { get; set; } = null;
    public string? Customer { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? ContactPerson { get; set; } = null;
    public string? DRNo { get; set; } = null;
    public int DocNum { get; set; }

    public DateTime PostingDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime DocumentDate { get; set; }
    public string? SchoolYear { get; set; } = null;
    public string? ActualDelivDate { get; set; } = null;
    public string? CourierName { get; set; } = null;
    public string? WayBillNo { get; set; } = null;
    public string? PlateNo { get; set; } = null;


    public IEnumerable<DeliveryLineDTO> DocumentLines { get; set; } = [];


}
