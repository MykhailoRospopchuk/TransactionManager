using TransactionManagement.Model.Enums;

namespace TransactionManagement.Model.RequestModel
{
    public class FilterTransactionCsvRequest
    {
        public StatusTransaction? Status { get; set; }
        public TypeTransaction? Type { get; set; }
    }
}
