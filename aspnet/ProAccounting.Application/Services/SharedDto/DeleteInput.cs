namespace ProAccounting.Application.Services.SharedDto
{
    public class DeleteInput<T> where T : struct
    {
        public T Id { get; set; }
    }

    public class DeleteIntInput : DeleteInput<int> { }
    public class DeleteLongInput : DeleteInput<long> { }
}
