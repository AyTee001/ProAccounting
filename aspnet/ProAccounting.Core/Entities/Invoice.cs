using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Core.Entities;

public partial class Invoice
{
    public long Id { get; set; }

    public int? ClientId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? DueDate { get; set; }

    public InvoiceStatus Status { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<InvoiceLog> InvoiceLogs { get; set; } = new List<InvoiceLog>();

    public virtual ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
