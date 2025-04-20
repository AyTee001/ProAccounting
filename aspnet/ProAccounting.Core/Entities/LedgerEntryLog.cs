namespace ProAccounting.Core.Entities;

public partial class LedgerEntryLog
{
    public long Id { get; set; }

    public long LedgerEntryId { get; set; }

    public string FieldName { get; set; } = null!;

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public DateTime ChangeDate { get; set; }

    public virtual LedgerEntry LedgerEntry { get; set; } = null!;
}
