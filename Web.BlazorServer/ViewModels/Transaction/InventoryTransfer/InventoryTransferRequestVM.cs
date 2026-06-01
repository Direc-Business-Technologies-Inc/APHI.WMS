using Application.DataTransferObjects.Others;
using Web.BlazorServer.ViewModels.Others;
using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.InventoryTransfer
{
    public class InventoryTransferRequestVM
    {
        public int? DocEntry { get; set; } = null;
        public int? DocNum { get; set; } = null;
        public DateTime DocDate { get; set; } = DateTime.Today;
        public WarehouseVM? FromWarehouse { get; set; } = null;
        public WarehouseVM? ToWarehouse { get; set; } = null;
        public TransferTypeVM? TransferType { get; set; } = null;

        public SchoolYearVM? SchoolYear { get; set; } = null;
        public string? Remarks { get; set; }
        public string? PreparedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? NotedBy { get; set; }
        public BusinessPartnerVM? BusinessPartner { get; set; } = null;
        public List<InventoryTransferRequestLineVM> Lines { get; set; } = [];

    }
}
