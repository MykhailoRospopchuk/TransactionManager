using MediatR;
using TransactionManagement.Model;

namespace TransactionManagement.Queries
{
    public record GetAllTransactionQuery() : IRequest<IEnumerable<TransactRecord>>;
}
