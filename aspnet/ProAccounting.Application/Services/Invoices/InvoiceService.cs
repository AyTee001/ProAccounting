using Microsoft.EntityFrameworkCore;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services.Invoices.Dto;
using ProAccounting.Core.Entities;
using ProAccounting.Core.Entities.Enums;
using ProAccounting.Core.Views;
using System.Text;

namespace ProAccounting.Application.Services.Invoices
{
    public class InvoiceService(ProAccountingDbContext dbContext) : IInvoiceService
    {
        private readonly ProAccountingDbContext _dbContext = dbContext;

        public async Task Create(CreateInvoiceInput input)
        {
            ValidateCreate(input);

            var clientExists = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM Clients WHERE Id = {0}) SELECT 1 ELSE SELECT 0", input.ClientId);

            if (clientExists == 0) throw new BusinessException("Client set on the invoice not found");

            long invoiceId = await _dbContext.Database.SqlQueryRaw<long>(
                "INSERT INTO Invoices (ClientId, Date, DueDate, Status) VALUES ({0}, {1}, {2}, {3});" +
                "SELECT CAST(SCOPE_IDENTITY() AS BIGINT);",
                input.ClientId, input.Date, input.DueDate, InvoiceStatus.Unpaid.ToString()).SingleAsync();

            await InsertInvoiceItems(input.InvoiceItems, invoiceId);
        }

        public async Task Delete(long id)
        {
            _ = await _dbContext.Database.ExecuteSqlRawAsync(
                "DELETE FROM Invoices WHERE Id = {0}", id);
        }

        public async Task<List<GetAllInvoiceData>> GetAll()
        {
            var result = await _dbContext.GetAllInvoiceData.FromSqlRaw("SELECT * FROM GetAllInvoiceData").ToListAsync();
            return result ?? [];
        }

        public async Task<InvoiceData> GetById(long id)
        {
            var invoice = (await _dbContext.Invoices
                .FromSqlRaw("SELECT * FROM Invoices WHERE Id = {0}", id)
                .FirstOrDefaultAsync()) ?? throw new BusinessException("Client not found");

            var invoiceItems = await _dbContext.InvoiceItems.FromSqlRaw("SELECT * FROM InvoiceItems WHERE InvoiceId = {0}", id).ToListAsync();

            return new InvoiceData
            {
                Id = invoice.Id,
                ClientId = invoice.ClientId,
                Date = invoice.Date,
                DueDate = invoice.DueDate,
                Status = invoice.Status,
                InvoiceItems = invoiceItems?.Select(x =>
                    new InvoiceItemData
                    {
                        Id = x.Id,
                        Description = x.Description,
                        InvoiceId = x.InvoiceId,
                        ProductName = x.ProductName,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice
                    }).ToList() ?? []
            };
        }

        public async Task Update(UpdateInvoiceInput input)
        {
            ValidateUpdate(input);

            var affected = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Invoices" +
                "SET Date = {0}, DueDate = {1}" +
                "WHERE Id = {2}",
                input.Id, input.Date, input.DueDate);

            if (affected == 0) throw new BusinessException("Invoice not found");

            var currentInvoiceItems = await _dbContext.InvoiceItems.FromSqlRaw("SELECT * FROM InvoiceItems WHERE InvoiceId = {0}", input.Id).ToListAsync();
            var currentInvoiceItemsIds = currentInvoiceItems.Select(x => x.Id).ToHashSet();

            var updatedItems = input.InvoiceItems.Where(x => x.Id.HasValue && currentInvoiceItemsIds.Contains((long)x.Id));
            var updatedItemsIds = updatedItems.Select(x => x.Id);

            var deletedItems = currentInvoiceItems.Where(x => !updatedItemsIds.Contains(x.Id));

            var createdInvoiceItems = input.InvoiceItems.Where(x => x.Id is null);

            await InsertInvoiceItems([.. createdInvoiceItems], input.Id);
            await UpdateInvoiceItems([.. updatedItems]);
        }

        private async Task InsertInvoiceItems(ICollection<EditInvoiceItemInput> invoiceItems, long invoiceId)
        {
            var insertInvoiceItemsQuery = new StringBuilder("INSERT INTO InvoiceItems (InvoiceId, Description, ProductName, Quantity, UnitPrice) VALUES ");

            var parameters = new List<object>();
            var valueList = new List<string>();

            int paramCounter = 0;
            foreach (var item in invoiceItems)
            {
                valueList.Add($"({{{paramCounter++}, {{{paramCounter++}, {{{paramCounter++}}}, {{{paramCounter++}}}, {{{paramCounter++}}})");
                parameters.Add(invoiceId);
                parameters.Add(item.Description);
                parameters.Add(item.ProductName);
                parameters.Add(item.Quantity);
                parameters.Add(item.UnitPrice);
            }

            insertInvoiceItemsQuery.Append(string.Join(",", valueList));
            await _dbContext.Database.ExecuteSqlRawAsync(insertInvoiceItemsQuery.ToString(), parameters.ToArray());
        }
        private async Task UpdateInvoiceItems(ICollection<EditInvoiceItemInput> invoiceItems)
        {
            var parameters = new List<object>();
            var queryList = new List<string>();

            int paramCounter = 0;
            foreach (var item in invoiceItems)
            {
                queryList.Add($"UPDATE InvoiceItems" +
                    $"SET Description = {{{paramCounter++}}}, ProductName = {{{paramCounter++}}}, Quantity = {{{paramCounter++}}}, UnitPrice = {{{paramCounter++}}}" +
                    $"WHERE Id = {{{paramCounter++}}})");
                parameters.Add(item.Description);
                parameters.Add(item.ProductName);
                parameters.Add(item.Quantity);
                parameters.Add(item.UnitPrice);
                parameters.Add((long)item.Id!);
            }

            await _dbContext.Database.ExecuteSqlRawAsync(string.Join(";", queryList), [.. parameters]);
        }

        public static void ValidateCreate(CreateInvoiceInput input)
        {
            if (input.Date == default)
                throw new BusinessException("Invoice date is required.");

            if (input.DueDate == default)
                throw new BusinessException("Due date is required.");

            if (input.DueDate <= input.Date)
                throw new BusinessException("Due date must be after invoice date.");

            if (input.InvoiceItems == null || input.InvoiceItems.Count == 0)
                throw new BusinessException("At least one invoice item is required.");

            foreach (var item in input.InvoiceItems)
            {
                ValidateItem(item);
            }
        }

        public static void ValidateUpdate(UpdateInvoiceInput input)
        {
            if (input.Date == default)
                throw new BusinessException("Invoice date is required.");

            if (input.DueDate == default)
                throw new BusinessException("Due date is required.");

            foreach (var item in input.InvoiceItems)
            {
                ValidateItem(item);
            }
        }

        private static void ValidateItem(EditInvoiceItemInput item)
        {
            if (string.IsNullOrWhiteSpace(item.Description))
                throw new BusinessException("Item description is required.");

            if (string.IsNullOrWhiteSpace(item.ProductName))
                throw new BusinessException("Product name is required.");

            if (item.Quantity <= 0)
                throw new BusinessException("Quantity must be greater than zero.");

            if (item.UnitPrice <= 0)
                throw new BusinessException("Unit price must be greater than zero.");
        }
    }
}
