namespace ProAccounting.Core.Entities;

public partial class InvoiceLog
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public string FieldName { get; set; } = null!;

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public DateTime ChangeDate { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
