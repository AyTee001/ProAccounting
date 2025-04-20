using ProAccounting.Application.Services.LedgerAccounts.Dto;

namespace ProAccounting.Application.Interfaces
{
    public interface ILedgerAccountService
    {
        Task Create(CreateLedgerAccountInput input);
        Task Update(UpdateLedgerAccountInput input);
        Task Delete(int id);
        Task<List<LedgerAccountData>> GetAll();
        Task<LedgerAccountData> GetById(int id);
    }
}
