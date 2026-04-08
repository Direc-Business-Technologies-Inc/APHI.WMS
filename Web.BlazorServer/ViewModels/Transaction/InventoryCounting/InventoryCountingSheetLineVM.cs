using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.InventoryCounting;

public class InventoryCountingSheetLineVM : ItemVM
{
    public string SheetNo { get; set; } = string.Empty;
}
