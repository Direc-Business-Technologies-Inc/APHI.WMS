
using Ardalis.GuardClauses;

namespace Domain.ValueObjects.Transaction;

public class SapDocumentReferenceVO : ValueObject
{
    public string? DocEntry { get; private set; }
    public string? DocNum { get; private set; }

    SapDocumentReferenceVO() { }

    public SapDocumentReferenceVO(string docEntry, string docNum)
    {
        DocEntry = docEntry;
        DocNum = docNum;
    }

    public SapDocumentReferenceVO SetDocEntry(string docEntry)
    {
        DocEntry = Guard.Against.NullOrEmpty(docEntry, nameof(docEntry), "Document Entry cannot be null or empty");
        return this;
    }

    public SapDocumentReferenceVO SetDocNum(string docNum)
    {
        DocNum = Guard.Against.NullOrEmpty(docNum, nameof(docNum), "Document Number cannot be null or empty");
        return this;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DocEntry;
        yield return DocNum;
    }

}
