using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Core.Entities;

public partial class Payment
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
