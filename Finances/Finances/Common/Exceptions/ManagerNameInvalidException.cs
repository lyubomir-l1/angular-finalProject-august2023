namespace Finances.Common.Exceptions
{
    public class ManagerNameInvalidException : Exception
    {
        public ManagerNameInvalidException(Exception ex)
            : base("Invalid manager name", ex)
        {
        }
    }
}
