namespace ProAccounting.Core.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
