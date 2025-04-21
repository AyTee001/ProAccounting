namespace ProAccounting.Application.Services.Invoices.Dto
{
    public class InvoiceItemData
    {
        public long Id { get; set; }

        public long InvoiceId { get; set; }

        public string Description { get; set; } = null!;

        public int Quantity { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal UnitPrice { get; set; }
    }
}
