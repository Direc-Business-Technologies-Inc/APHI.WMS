using Application.DataTransferObjects.System.Commons;
using Application.DataTransferObjects.Transactions.Commons;
using Domain.Enums.Transaction.Commons;

namespace Web.BlazorServer.ViewModels.Transaction.Commons;

public class TransactionalDocumentVM
{
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.None;
    public AppDocNumVM AppDocNum { get; set; } = new();
    public SapDocumentReferenceVM SapReference { get; set; } = new();
    public DocumentTypeVM DocumentType { get; set; } = new();
}
