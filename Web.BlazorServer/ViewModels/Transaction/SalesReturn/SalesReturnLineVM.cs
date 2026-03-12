using Web.BlazorServer.ViewModels.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.SalesReturn;

public class SalesReturnLineVM : ItemVM
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
}
