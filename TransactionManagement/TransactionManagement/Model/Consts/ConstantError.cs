namespace TransactionManagement.Model.Consts
{
    public class ConstantError
    {
        public static string GetErrorForException(string type, int id)
            => $"{type} with id {id} doesn't exist.";

        public static string GetCredentialsErrorExceptionMessage(string type, string login, string password)
            => $"{type} with login {login} and password {password} does not exist.";

        public static string GetNullForException()
            => "The Transaction collection is empty";

        public static string GetSomethingWrongForException(string message)
            => "Something went wrong in";

        public static string CsvHandlerErrorForException(string method, string message)
            => $"Something went wrong in: {method}; message: {message}";
    }
}
