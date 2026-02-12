using Ardalis.GuardClauses;

namespace Domain.ValueObjects.Transaction;

public class LsmsDocNumVO : ValueObject
{
    public string Value { get; private set; }

    LsmsDocNumVO() { }

    public LsmsDocNumVO(string value)
    {
        Value = Guard.Against.NullOrEmpty(value, nameof(value), "LSMS Document Number cannot be null or empty");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
