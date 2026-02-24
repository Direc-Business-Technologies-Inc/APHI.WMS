using Ardalis.GuardClauses;

namespace Integration.SAP.Entities.Transactional.GoodsIssue;

public class InventoryGenExitPayload
{
    public string U_PrepBy { get; private set; }
    public string Comments { get; private set; }
    public string U_TransType { get; private set; }
    public IEnumerable<InventoryGenExitLinesPayload> DocumentLines { get; private set; } = [];

    public InventoryGenExitPayload(string preparedBy, string transType, IEnumerable<InventoryGenExitLinesPayload> lines)
    {
        U_PrepBy = Guard.Against.NullOrEmpty(preparedBy, nameof(U_PrepBy), "Prepared By cannot be null or empty");
        U_TransType = Guard.Against.NullOrEmpty(transType, nameof(U_TransType), "Transaction Type cannot be null or empty");
        DocumentLines = Guard.Against.NullOrEmpty(lines, nameof(DocumentLines), "Document Lines cannot be null or empty");
        Comments = "Posted from WMS";
    }
}
