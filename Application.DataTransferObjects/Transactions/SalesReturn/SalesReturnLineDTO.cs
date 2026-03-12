using Application.DataTransferObjects.Transactions.Commons;

namespace Application.DataTransferObjects.Transactions.SalesReturn;

public class SalesReturnLineDTO : ItemDTO
{
    public int DocEntry { get; set; }
    public int DocNum { get; set; }
}
