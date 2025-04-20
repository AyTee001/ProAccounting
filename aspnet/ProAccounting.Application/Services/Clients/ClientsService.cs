using Microsoft.EntityFrameworkCore;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services.Clients.Dto;
using ProAccounting.Application.Services.SharedDto;

namespace ProAccounting.Application.Services.Clients
{
    public class ClientsService(ProAccountingDbContext dbContext) : IClientService
    {
        private readonly ProAccountingDbContext _dbContext = dbContext;

        public async Task Create(CreateClientInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Email))
            {
                throw new BusinessException("Both email and name must be filled");
            }

            var nameExists = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM Clients WHERE Name = {0}) SELECT 1 ELSE SELECT 0", input.Name);

            if (nameExists == 1) throw new BusinessException("Client with this name already exists");

            var emailExists = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM Clients WHERE Email = {0}) SELECT 1 ELSE SELECT 0", input.Email);

            if (emailExists == 1) throw new BusinessException("Client with this email already exists");

            await _dbContext.Database.ExecuteSqlRawAsync(
                "INSERT INTO Clients (Name, Email, Address, PhoneNumber) VALUES ({0}, {1}, {2}, {3})",
                input.Name, input.Email, input.Address!, input.PhoneNumber!);
        }

        public async Task Delete(int id)
        {
            var clientHasInvoices = await _dbContext.Database.ExecuteSqlRawAsync(
                "IF EXISTS (SELECT 1 FROM Invoices WHERE ClientId = {0}) SELECT 1 ELSE SELECT 0", id);

            if (clientHasInvoices == 1)
            {
                throw new BusinessException("Cannot delete client with associated invoices");
            }

            _ = await _dbContext.Database.ExecuteSqlRawAsync(
                "DELETE FROM Clients WHERE Id = {0}", id);
        }

        public async Task<List<ClientData>> GetAll()
        {
            var result = await _dbContext.Clients.FromSqlRaw("SELECT * FROM Clients").ToListAsync();
            return result.Select(c => new ClientData { Id = c.Id, Name = c.Name, Email = c.Email, PhoneNumber = c.PhoneNumber, Address = c.Address }).ToList();
        }

        public async Task<ClientData> GetById(int id)
        {
            var client = await _dbContext.Clients
                .FromSqlRaw("SELECT * FROM Clients WHERE Id = {0}", id)
                .FirstOrDefaultAsync();

            return client == null
                ? throw new BusinessException("Client not found")
                : new ClientData { Id = client.Id, Name = client.Name, Email = client.Email, PhoneNumber = client.PhoneNumber, Address = client.Address };
        }

        public async Task Update(UpdateClientInput input)
        {
            var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE Clients SET Name = {0}, Email = {1}, Address = {2}, PhoneNumber = {3} WHERE Id = {4}",
                input.Name, input.Email, input.Address!, input.PhoneNumber!, input.Id);

            if (rowsAffected == 0)
                throw new BusinessException("Client not found");
        }
    }
}
