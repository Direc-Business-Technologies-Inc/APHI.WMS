using Ardalis.GuardClauses;

namespace Integration.SAP.Entities.Transactional.Receiving;

public class GoodsReceiptPO
{
    public string CardCode { get; private set; }
    public DateTime DocDate { get; private set; }
    public DateTime DocDueDate { get; private set; }
    public DateTime TaxDate { get; private set; }
    public string U_RecBy { get; private set; }
    public string Comments { get; private set; }
    public List<GoodsReceiptPOLines> DocumentLines { get; private set; } = [];

    public GoodsReceiptPO(string cardCode,
                          DateTime docDate,
                          DateTime docDueDate,
                          DateTime taxDate,
                          string recBy,
                          List<GoodsReceiptPOLines> documentLines)
    {
        CardCode = Guard.Against.NullOrEmpty(cardCode, nameof(CardCode), "Card Code cannot be null or empty");
        DocDate = Guard.Against.NullOrOutOfSQLDateRange(docDate, nameof(DocDate), "Doc Date cannot be null or out of range");
        DocDueDate = Guard.Against.NullOrOutOfSQLDateRange(docDueDate, nameof(DocDueDate), "Doc Due Date cannot be null or out of range");
        TaxDate = Guard.Against.NullOrOutOfSQLDateRange(taxDate, nameof(TaxDate), "Tax Date cannot be null or out of range");
        U_RecBy = Guard.Against.NullOrEmpty(recBy, nameof(U_RecBy), "Received By cannot be null or empty");
        DocumentLines = [.. Guard.Against.NullOrEmpty(documentLines, nameof(DocumentLines), "Document Lines cannot be null or empty")];
        Comments = "Posted from WMS";
    }
}
