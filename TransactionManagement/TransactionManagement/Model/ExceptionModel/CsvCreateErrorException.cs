namespace TransactionManagement.Model.ExceptionModel
{
    public class CsvCreateErrorException : Exception
    {
        public CsvCreateErrorException(string message)
            : base(message)
        { }
    }
}
