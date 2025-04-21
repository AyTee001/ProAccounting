namespace ProAccounting.Application.Services.Invoices.Dto
{
    public class CreateInvoiceInput
    {
        public int ClientId { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public ICollection<EditInvoiceItemInput> InvoiceItems { get; set; } = [];
    }
}
