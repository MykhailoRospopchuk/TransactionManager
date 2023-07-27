using TransactionManagement.Model.Enums;

namespace TransactionManagement.Model.RequestModel
{
    public class UpdateTransactionRequest
    {
        public int Id { get; set; }
        public StatusTransaction Status { get; set; }
    }
}
