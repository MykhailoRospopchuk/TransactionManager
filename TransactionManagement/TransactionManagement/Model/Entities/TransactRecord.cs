using TransactionManagement.Model.Enums;

namespace TransactionManagement.Model.Entities
{
    public class TransactRecord
    {
        public int TransactionId { get; set; }
        public StatusTransaction Status { get; set; }
        public TypeTransaction Type { get; set; }
        public string ClientName { get; set; }
        public double Amount { get; set; }
    }
}
