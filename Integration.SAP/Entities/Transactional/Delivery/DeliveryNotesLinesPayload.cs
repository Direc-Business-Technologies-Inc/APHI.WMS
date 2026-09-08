using Ardalis.GuardClauses;

namespace Integration.SAP.Entities.Transactional.Delivery;

public class DeliveryNotesLinesPayload
{
    public int BaseEntry { get; private set; }
    public int BaseType { get; private set; }
    public int BaseLine { get; private set; }
    public int LineNum { get; private set; }
    public string ItemCode { get; private set; }
    public decimal Quantity { get; private set; }
    public string WarehouseCode { get; private set; }
    public decimal U_MarkUp { get; private set; }
    public IEnumerable<DeliveryNotesLineAdditionalExpensesPayload>? DocumentLineAdditionalExpenses { get; private set; }

    public DeliveryNotesLinesPayload(int baseEntry,
                                     int baseType,
                                     int baseLine,
                                     int lineNum,
                                     string itemCode,
                                     decimal quantity,
                                     string warehouseCode,
                                     decimal markup,
                                     List<DeliveryNotesLineAdditionalExpensesPayload>? additionalExpenses)
    {
        BaseEntry = Guard.Against.Negative(baseEntry, nameof(BaseEntry), "Base Entry cannot be negative");
        BaseType = Guard.Against.Negative(baseType, nameof(BaseType), "Base Type cannot be negative");
        BaseLine = Guard.Against.Negative(baseLine - 1, nameof(BaseLine), "Base Line cannot be negative");
        LineNum = Guard.Against.Negative(lineNum, nameof(LineNum), "Line Num cannot be negative");
        ItemCode = Guard.Against.NullOrEmpty(itemCode, nameof(ItemCode), "Item Code cannot be null or empty");
        Quantity = Guard.Against.NegativeOrZero(quantity, nameof(Quantity), "Quantity cannot be negative or zero");
        WarehouseCode = Guard.Against.NullOrEmpty(warehouseCode, nameof(WarehouseCode), "Warehouse Code cannot be null or empty");
        U_MarkUp = Guard.Against.Negative(markup, nameof(U_MarkUp), "Markup cannot be negative");
        DocumentLineAdditionalExpenses = additionalExpenses;
    }

    public class DeliveryNotesLineAdditionalExpensesPayload
    {
        public int LineNumber { get; private set; }
        public int GroupCode { get; private set; }
        public int ExpenseCode { get; private set; }
        public decimal LineTotal { get; private set; }

        public DeliveryNotesLineAdditionalExpensesPayload(int lineNumber, int groupCode, int expenseCode, decimal lineTotal)
        {
            LineNumber = Guard.Against.Negative(lineNumber, nameof(LineNumber), "Line Number cannot be negative");
            GroupCode = Guard.Against.Negative(groupCode, nameof(GroupCode), "GroupCode cannot be negative");
            ExpenseCode = Guard.Against.NegativeOrZero(expenseCode, nameof(ExpenseCode), "ExpenseCode cannot be negative or zero");
            LineTotal = lineTotal;
        }
    }
}
