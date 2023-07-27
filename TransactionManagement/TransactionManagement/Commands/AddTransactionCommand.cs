using MediatR;
using TransactionManagement.Model.Entities;

namespace TransactionManagement.Commands
{
    public record AddTransactionCommand(IEnumerable<TransactRecord> transactRecord) : IRequest;
}
