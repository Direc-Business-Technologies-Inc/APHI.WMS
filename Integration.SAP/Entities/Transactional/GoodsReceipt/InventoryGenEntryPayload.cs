using Ardalis.GuardClauses;
using Integration.SAP.Entities.Transactional.GoodsIssue;

namespace Integration.SAP.Entities.Transactional.GoodsReceipt;

public class InventoryGenEntryPayload
{
    public string U_PrepBy { get; private set; }
    public string Comments { get; private set; }
    public string U_TransType { get; private set; }
    public IEnumerable<InventoryGenEntryLinesPayload> DocumentLines { get; private set; } = [];

    public InventoryGenEntryPayload(string preparedBy, string transType, IEnumerable<InventoryGenEntryLinesPayload> lines)
    {
        U_PrepBy = Guard.Against.NullOrEmpty(preparedBy, nameof(U_PrepBy), "Prepared By cannot be null or empty");
        U_TransType = Guard.Against.NullOrEmpty(transType, nameof(U_TransType), "Transaction Type cannot be null or empty");
        DocumentLines = Guard.Against.NullOrEmpty(lines, nameof(DocumentLines), "Document Lines cannot be null or empty");
        Comments = "Posted from WMS";
    }
}
