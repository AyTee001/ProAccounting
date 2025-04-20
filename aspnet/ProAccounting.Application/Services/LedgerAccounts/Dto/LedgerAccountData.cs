using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Application.Services.LedgerAccounts.Dto
{
    public class LedgerAccountData
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; } = null!;

        public LedgerType LedgerType { get; set; }

    }
}
