CREATE OR ALTER VIEW GetAllInvoiceData AS
SELECT 
    inv.Id,
    inv.ClientId,
    inv.Date,
    inv.DueDate,
    inv.Status,
    SUM(it.UnitPrice * it.Quantity) AS Amount
FROM Invoices inv
LEFT JOIN InvoiceItems it ON inv.Id = it.InvoiceId
GROUP BY inv.Id, inv.ClientId, inv.Date, inv.DueDate, inv.Status;
