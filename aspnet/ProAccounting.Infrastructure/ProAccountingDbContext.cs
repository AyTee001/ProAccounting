using Microsoft.EntityFrameworkCore;
using ProAccounting.Core.Entities;
using ProAccounting.Core.Entities.Enums;
using ProAccounting.Core.Views;
namespace ProAccounting;

public partial class ProAccountingDbContext : DbContext
{

    public ProAccountingDbContext()
    {
    }

    public ProAccountingDbContext(DbContextOptions<ProAccountingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<InvoiceLog> InvoiceLogs { get; set; }

    public virtual DbSet<LedgerAccount> LedgerAccounts { get; set; }

    public virtual DbSet<LedgerEntry> LedgerEntries { get; set; }

    public virtual DbSet<LedgerEntryLog> LedgerEntryLogs { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<GetAllInvoiceData> GetAllInvoiceData { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GetAllInvoiceData>()
            .HasNoKey()
            .ToView("vw_GetAllInvoiceData");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(500);

            entity.Property(e => e.Name)
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(e => e.Email)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.ClientId);

            entity.ToTable(x => x.HasCheckConstraint("CK_Invoice_Status",
                $"Status IN ({EnumList<InvoiceStatus>()})"));

            entity.HasOne(d => d.Client).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Status)
                .HasConversion(
                    e => e.ToString(),
                    e => Enum.Parse<InvoiceStatus>(e)
                );
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.InvoiceId);

            entity.Property(e => e.Description)
                .HasMaxLength(255);

            entity.Property(e => e.ProductName)
                .HasMaxLength(100);

            entity.Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InvoiceLog>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.InvoiceId);

            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.FieldName)
                .HasMaxLength(50);

            entity.Property(e => e.NewValue)
                .HasMaxLength(255);

            entity.Property(e => e.OldValue)
                .HasMaxLength(255);

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLogs)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LedgerAccount>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Code).IsUnique();

            entity.HasIndex(e => e.Name).IsUnique();

            entity.ToTable(x => x.HasCheckConstraint("CK_Ledger_Type",
                $"LedgerType IN ({EnumList<LedgerType>()})"));

            entity.Property(e => e.Name)
                .HasMaxLength(100);

            entity.Property(e => e.LedgerType)
                .HasConversion(
                    e => e.ToString(),
                    e => Enum.Parse<LedgerType>(e)
                );
        });

        modelBuilder.Entity<LedgerEntry>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.InvoiceId);

            entity.HasIndex(e => e.LedgerAccountId);

            entity.Property(e => e.Description)
                .HasMaxLength(255);

            entity.Property(e => e.Credit)
                .HasPrecision(18, 2);

            entity.Property(e => e.Debit)
                .HasPrecision(18, 2);

            entity.HasOne(d => d.Invoice).WithMany(p => p.LedgerEntries)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.LedgerAccount).WithMany(p => p.LedgerEntries)
                .HasForeignKey(d => d.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LedgerEntryLog>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.LedgerEntryId);

            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.FieldName)
                .HasMaxLength(50);

            entity.Property(e => e.NewValue)
                .HasMaxLength(255);

            entity.Property(e => e.OldValue)
                .HasMaxLength(255);

            entity.HasOne(d => d.LedgerEntry).WithMany(p => p.LedgerEntryLogs)
                .HasForeignKey(d => d.LedgerEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.InvoiceId);

            entity.ToTable(x => x.HasCheckConstraint("CK_Payment_Method",
                $"PaymentMethod IN ({EnumList<PaymentMethod>()})"));

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.Amount)
                .HasPrecision(18, 2);

            entity.Property(x => x.PaymentMethod)
                .HasConversion(
                    e => e.ToString(),
                    e => Enum.Parse<PaymentMethod>(e)
                );
        });
    }

    private static string EnumList<T>() where T : Enum
    {
        return string.Join(",", Enum.GetNames(typeof(T)).Select(e => $"'{e}'"));
    }
}
