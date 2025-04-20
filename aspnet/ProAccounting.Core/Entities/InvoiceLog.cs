namespace ProAccounting.Core.Entities;

public partial class InvoiceLog
{
    public long Id { get; set; }

    public long? InvoiceId { get; set; }

    public string? FieldName { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime? ChangeDate { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
