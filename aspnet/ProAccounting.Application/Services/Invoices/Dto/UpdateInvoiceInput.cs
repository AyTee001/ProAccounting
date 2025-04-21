namespace ProAccounting.Application.Services.Invoices.Dto
{
    public class UpdateInvoiceInput
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public ICollection<EditInvoiceItemInput> InvoiceItems { get; set; } = [];
    }
}
