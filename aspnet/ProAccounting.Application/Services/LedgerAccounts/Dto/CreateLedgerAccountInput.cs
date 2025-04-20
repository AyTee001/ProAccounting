using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Application.Services.LedgerAccounts.Dto
{
    public class CreateLedgerAccountInput
    {
        public int Code { get; set; }

        public string Name { get; set; } = null!;

        public LedgerType LedgerType { get; set; }

    }
}
