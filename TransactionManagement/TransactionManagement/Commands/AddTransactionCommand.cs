using MediatR;
using TransactionManagement.Model;

namespace TransactionManagement.Commands
{
    public record AddTransactionCommand(IEnumerable<TransactRecord> transactRecord) : IRequest;
}
