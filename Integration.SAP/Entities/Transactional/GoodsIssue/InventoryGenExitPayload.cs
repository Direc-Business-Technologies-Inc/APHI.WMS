using Ardalis.GuardClauses;

namespace Integration.SAP.Entities.Transactional.GoodsIssue;

public class InventoryGenExitPayload
{
    public string U_PrepBy { get; private set; }
    public string Comments { get; private set; }
    public string U_TransType { get; private set; }
    public string? U_BpCode { get; private set; }
    public string? U_BPName { get; private set; }
    public string? U_SchlYear { get; private set; }
    public string? U_SRFNo { get; private set; }
    public string? U_Desig { get; set; }
    public string? U_Remarks { get; set; }
    public string? U_AppBy { get; set; }
    public string? U_RecBy { get; set; }
    public string? U_NotedBy { get; set; }
    public IEnumerable<InventoryGenExitLinesPayload> DocumentLines { get; private set; } = [];

    public InventoryGenExitPayload(string preparedBy,
                                   string transType,
                                   IEnumerable<InventoryGenExitLinesPayload> lines,
                                   string? bpCode = null,
                                   string? bpName = null,
                                   string? schlyear = null,
                                   string? srfNo = null,
                                   string? designation = null,
                                   string? remarks = null,
                                   string? appBy = null,
                                   string? recBy = null,
                                   string? notedBy = null)
    {
        U_PrepBy = Guard.Against.NullOrEmpty(preparedBy, nameof(U_PrepBy), "Prepared By cannot be null or empty");
        U_TransType = Guard.Against.NullOrEmpty(transType, nameof(U_TransType), "Transaction Type cannot be null or empty");
        DocumentLines = Guard.Against.NullOrEmpty(lines, nameof(DocumentLines), "Document Lines cannot be null or empty");
        Comments = "Posted from WMS";
        U_BpCode = transType.Trim().ToLowerInvariant().Equals("adjustments") ?
            Guard.Against.NullOrEmpty(bpCode, nameof(U_BpCode), "U_BpCode cannot be null or empty") :
            bpCode != null ?
                throw new Exception("Cannot use BP Code when transaction type is adjustment") :
                null;
        U_BPName = transType.Trim().ToLowerInvariant().Equals("samples") ?
            Guard.Against.NullOrEmpty(bpName, nameof(U_BPName), "U_BPName cannot be null or empty") :
            bpName;
        U_SchlYear = schlyear;
        U_SRFNo = srfNo;
        U_Desig = transType.Trim().ToLowerInvariant().Equals("samples") ?
            Guard.Against.NullOrEmpty(designation, nameof(U_BPName), "U_Desig cannot be null or empty") : 
            designation;
        U_Remarks = remarks;
        U_AppBy = appBy;
        U_RecBy = transType.Trim().ToLowerInvariant().Equals("adjustments") ? 
            recBy :
            Guard.Against.NullOrEmpty(recBy, nameof(U_RecBy), "U_RecBy cannot be null or empty");
        U_NotedBy = notedBy;
    }
}
