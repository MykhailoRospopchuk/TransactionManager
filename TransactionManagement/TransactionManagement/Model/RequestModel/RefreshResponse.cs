namespace TransactionManagement.Model.RequestModel
{
    public class RefreshResponse
    {
        public string NewJwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
