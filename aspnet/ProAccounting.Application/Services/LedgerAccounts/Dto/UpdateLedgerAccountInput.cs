using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Application.Services.LedgerAccounts.Dto
{
    public class UpdateLedgerAccountInput
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

    }
}
