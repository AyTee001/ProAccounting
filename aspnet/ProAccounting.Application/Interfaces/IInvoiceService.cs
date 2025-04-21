using ProAccounting.Application.Services.Invoices.Dto;
using ProAccounting.Core.Views;

namespace ProAccounting.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task Create(CreateInvoiceInput input);
        Task Update(UpdateInvoiceInput input);
        Task Delete(long id);
        Task<List<GetAllInvoiceData>> GetAll();
        Task<InvoiceData> GetById(long id);
    }
}
