using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Core.Entities;

public partial class LedgerAccount
{
    public int Id { get; set; }

    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public LedgerType LedgerType { get; set; }

    public virtual ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();
}
