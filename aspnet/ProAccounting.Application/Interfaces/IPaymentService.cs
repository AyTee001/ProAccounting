namespace ProAccounting.Application.Interfaces
{
    public interface IPaymentService
    {
        Task Create();
        Task Update();
        Task Delete();
        Task GetAll();
        Task GetById();
    }
}
