using ProAccounting.Core.Entities;
using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Application.Services.Invoices.Dto
{
    public class InvoiceData
    {
        public long Id { get; set; }

        public int ClientId { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public InvoiceStatus Status { get; set; }

        public ICollection<InvoiceItemData> InvoiceItems { get; set; } = [];
    }
}
