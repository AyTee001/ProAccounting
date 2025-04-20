namespace ProAccounting.Core.Entities;

public partial class InvoiceItem
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
