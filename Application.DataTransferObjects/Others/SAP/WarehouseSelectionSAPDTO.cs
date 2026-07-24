namespace Application.DataTransferObjects.Others.SAP;

public class WarehouseSelectionSAPDTO
{
    public string WhsCode { get; set; }
    public string WhsName { get; set; }
    public string Block { get; set; } = string.Empty;
}
