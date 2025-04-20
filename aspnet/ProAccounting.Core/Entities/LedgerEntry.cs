namespace ProAccounting.Core.Entities;

public partial class LedgerEntry
{
    public long Id { get; set; }

    public int LedgerAccountId { get; set; }

    public long InvoiceId { get; set; }

    public string Description { get; set; } = null!;

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual LedgerAccount LedgerAccount { get; set; } = null!;

    public virtual ICollection<LedgerEntryLog> LedgerEntryLogs { get; set; } = [];
}
