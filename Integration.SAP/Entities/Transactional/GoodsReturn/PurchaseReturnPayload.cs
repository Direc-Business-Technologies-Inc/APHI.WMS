using Ardalis.GuardClauses;

namespace Integration.SAP.Entities.Transactional.GoodsReturn;

public class PurchaseReturnPayload
{
    public DateTime DocDate { get; private set; }
    public DateTime DocDueDate { get; private set; }
    public string CardCode { get; private set; }
    public string U_PrepBy { get; private set; }
    public string Comments { get; private set; }

    public List<PurchaseReturnLinesPayload> DocumentLines { get; private set; } = [];

    public PurchaseReturnPayload(DateTime docDate,
                       DateTime docDueDate,
                       string cardCode,
                       string prepBy,
                       string comments,
                       List<PurchaseReturnLinesPayload> documentLines)
    {
        DocDate = Guard.Against.NullOrOutOfSQLDateRange(docDate, nameof(DocDate), "Doc Date cannot be null or out of range");
        DocDueDate = Guard.Against.NullOrOutOfSQLDateRange(docDueDate, nameof(DocDueDate), "Doc Due Date cannot be null or out of range");
        CardCode = Guard.Against.NullOrEmpty(cardCode, nameof(CardCode), "Card Code cannot be null or empty");
        U_PrepBy = Guard.Against.NullOrEmpty(prepBy, nameof(U_PrepBy), "Prepared By cannot be null or empty");
        Comments = "Posted from WMS";

        DocumentLines = [.. Guard.Against.NullOrEmpty(documentLines, nameof(DocumentLines), "Document Lines cannot be null or empty")];
    }
}
