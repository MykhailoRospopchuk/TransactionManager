namespace TransactionManagement.Model.ExceptionModel
{
    public class CsvReadErrorException : Exception
    {
        public CsvReadErrorException(string message)
            : base(message)
        { }
    }
}
