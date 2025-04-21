using ProAccounting.Core.Entities.Enums;

namespace ProAccounting.Core.Views
{
    public class GetAllInvoiceData
    {
        public long Id { get; set; }

        public int ClientId { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public InvoiceStatus Status { get; set; }

        public double Total { get; set; }
    }
}
