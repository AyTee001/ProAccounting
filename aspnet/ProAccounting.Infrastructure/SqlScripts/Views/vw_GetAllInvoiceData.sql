CREATE OR ALTER VIEW GetAllInvoiceData AS
SELECT 
    inv.Id,
    cl.Name AS Client,
    inv.Date,
    inv.DueDate,
    inv.Status,
    SUM(it.UnitPrice * it.Quantity) AS Total
FROM Invoices inv
LEFT JOIN InvoiceItems it ON inv.Id = it.InvoiceId
LEFT JOIN Clients cl ON inv.ClientId = cl.Id
GROUP BY inv.Id, inv.ClientId, inv.Date, inv.DueDate, inv.Status, cl.Name;
