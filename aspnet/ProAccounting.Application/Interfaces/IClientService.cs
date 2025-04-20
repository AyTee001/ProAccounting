using ProAccounting.Application.Services.Clients.Dto;
using ProAccounting.Application.Services.SharedDto;

namespace ProAccounting.Application.Interfaces
{
    public interface IClientService
    {
        Task Create(CreateClientInput input);
        Task Update(UpdateClientInput input);
        Task Delete(int id);
        Task<List<ClientData>> GetAll();
        Task<ClientData> GetById(int id);
    }
}
