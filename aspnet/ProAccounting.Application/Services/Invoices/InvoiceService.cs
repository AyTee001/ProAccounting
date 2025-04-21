using Microsoft.EntityFrameworkCore;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services.Clients.Dto;
using ProAccounting.Application.Services.Invoices.Dto;
using ProAccounting.Core.Views;

namespace ProAccounting.Application.Services.Invoices
{
    public class InvoiceService(ProAccountingDbContext dbContext) : IInvoiceService
    {
        private readonly ProAccountingDbContext _dbContext = dbContext;

        public Task Create(CreateInvoiceInput input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllInvoiceData>> GetAll()
        {
            var result = await _dbContext.GetAllInvoiceData.FromSqlRaw("SELECT * FROM GetAllInvoiceData").ToListAsync();
            return result ?? [];
        }

        public Task<InvoiceData> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdateInvoiceItemInput input)
        {
            throw new NotImplementedException();
        }
    }
}
