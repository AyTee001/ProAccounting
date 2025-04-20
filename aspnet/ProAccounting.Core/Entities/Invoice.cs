using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Core.Entities;

public partial class Invoice
{
    public long Id { get; set; }

    public int ClientId { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    public InvoiceStatus Status { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = [];

    public virtual ICollection<InvoiceLog> InvoiceLogs { get; set; } = [];

    public virtual ICollection<LedgerEntry> LedgerEntries { get; set; } = [];

    public virtual ICollection<Payment> Payments { get; set; } = [];
}
