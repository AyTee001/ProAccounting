namespace ProAccounting.Application
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string userFriendlyMessage) : base(userFriendlyMessage) { }
    }
}
