using TransactionManagement.Model.Enums;

namespace TransactionManagement.Model.RequestModel
{
    public class FilterTransactionRequest
    {
        public HashSet<StatusTransaction>? Status { get; set; }
        public TypeTransaction? Type { get; set; }
        public string ClientName { get; set; } = "";
    }
}
