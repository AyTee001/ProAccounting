using Microsoft.EntityFrameworkCore;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services.LedgerAccounts.Dto;

namespace ProAccounting.Application.Services.LedgerAccounts
{
    public class LedgerAccountService(ProAccountingDbContext dbContext) : ILedgerAccountService
    {
        private readonly ProAccountingDbContext _dbContext = dbContext;

        public async Task Create(CreateLedgerAccountInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new BusinessException("Name must not be empty");
            }

            var codeExists = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM LedgerAccounts WHERE Code = {0}) SELECT 1 ELSE SELECT 0", input.Code);

            if (codeExists == 1) throw new BusinessException("Ledger account with the following code already exists");

            var nameExists = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM LedgerAccounts WHERE Name = {0}) SELECT 1 ELSE SELECT 0", input.Name);

            if (nameExists == 1) throw new BusinessException("Ledger account with the following name already exists");

            await _dbContext.Database.ExecuteSqlRawAsync(
                "INSERT INTO LedgerAccounts (Name, Code, LedgerType) VALUES ({0}, {1}, {2})",
                input.Name, input.Code, input.LedgerType.ToString());
        }

        public async Task Delete(int id)
        {
            var ledgerAccountHasEntries = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM LedgerEntries WHERE LedgerAccountId = {0}) SELECT 1 ELSE SELECT 0", id);

            if (ledgerAccountHasEntries == 1)
            {
                throw new BusinessException("Cannot delete ledger account with associated ledger entries");
            }

            _ = await _dbContext.Database.ExecuteSqlRawAsync(
                "DELETE FROM LedgerAccounts WHERE Id = {0}", id);

        }

        public async Task<List<LedgerAccountData>> GetAll()
        {
            var result = await _dbContext.LedgerAccounts.FromSqlRaw("SELECT * FROM LedgerAccounts").ToListAsync();
            return result.Select(c => new LedgerAccountData { Id = c.Id, Name = c.Name, Code = c.Code, LedgerType = c.LedgerType }).ToList();
        }

        public async Task<LedgerAccountData> GetById(int id)
        {
            var acc = await _dbContext.LedgerAccounts
                .FromSqlRaw("SELECT * FROM LedgerAccounts WHERE Id = {0}", id)
                .FirstOrDefaultAsync();

            return acc == null
                ? throw new BusinessException("Ledger account not found")
                : new LedgerAccountData { Id = acc.Id, Name = acc.Name, Code = acc.Code, LedgerType = acc.LedgerType };
        }

        public async Task Update(UpdateLedgerAccountInput input)
        {
            _ = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE LedgerAccounts SET Name = {0}",
                input.Name);
        }
    }
}
