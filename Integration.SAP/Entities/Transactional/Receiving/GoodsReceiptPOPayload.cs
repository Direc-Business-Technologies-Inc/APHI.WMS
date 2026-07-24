using Ardalis.GuardClauses;

namespace Integration.SAP.Entities.Transactional.Receiving;

public class GoodsReceiptPOPayload
{
    public string CardCode { get; private set; }
    public string Comments { get; private set; }
    public string? U_SchlYear { get; private set; }
    public string? U_PONo { get; private set; }
    public string? U_DRNo { get; private set; }
    public int? U_Time { get; private set; }
    public string? U_SINo { get; private set; }
    public string? U_PurchType { get; private set; }
    public string? U_ItemName { get; private set; }
    public string? U_DelBy { get; private set; }
    public string? U_RecBy { get; private set; }
    public string? U_Remarks { get; private set; }
    public string? U_PrepBy { get; private set; }
    public string? U_RevBy { get; private set; }
    public string? U_AppBy { get; private set; }
    public string? U_NotedBy { get; private set; }
    public List<object> DocumentLines { get; private set; } = [];
    public string DocObjectCode { get; } = "oPurchaseDeliveryNotes";

    public GoodsReceiptPOPayload(string cardCode,
                          DateTime docDate,
                          DateTime docDueDate,
                          DateTime taxDate,
                          string prepBy,
                          List<object> documentLines,
                          string? schlYear = null,
                          string? poNo = null,
                          string? drNo = null,
                          int? time = null,
                          string? siNo = null,
                          string? purchType = null,
                          string? itemName = null,
                          string? delBy = null,
                          string? recBy = null,
                          string? docRemarks = null,
                          string? revBy = null,
                          string? appBy = null,
                          string? notedBy = null)
    {
        CardCode = Guard.Against.NullOrEmpty(cardCode, nameof(CardCode), "Card Code cannot be null or empty");
        U_PrepBy = Guard.Against.NullOrEmpty(prepBy, nameof(U_PrepBy), "Prepared By cannot be null or empty");
        DocumentLines = [.. Guard.Against.NullOrEmpty(documentLines, nameof(DocumentLines), "Document Lines cannot be null or empty")];
        Comments = "Posted from WMS";
        U_SchlYear = schlYear;
        U_PONo = poNo;
        U_DRNo = Guard.Against.NullOrEmpty(drNo, nameof(U_DRNo), "DR No. cannot be null or empty");
        U_Time = time;
        U_SINo = siNo;
        U_DelBy = Guard.Against.NullOrEmpty(delBy, nameof(U_DelBy), "Delivered By cannot be null or empty");
        U_RecBy = Guard.Against.NullOrEmpty(recBy, nameof(U_DelBy), "Received By cannot be null or empty");
        U_Remarks = docRemarks;
        U_PrepBy = Guard.Against.NullOrEmpty(prepBy, nameof(U_PrepBy), "Prepared By cannot be null or empty");
        U_RevBy = Guard.Against.NullOrEmpty(revBy, nameof(U_RevBy), "Reviewed By cannot be null or empty");
        U_AppBy = appBy;
        U_NotedBy = notedBy;
        U_Time = Guard.Against.Null(time, nameof(U_Time), "Time cannot be null or empty");
        U_PurchType = purchType;
        U_ItemName = itemName;
        // required fields: U_DRNo, U_DelBy, U_Time, U_PrepBy, U_RevBy, U_RecBy
    }
}
