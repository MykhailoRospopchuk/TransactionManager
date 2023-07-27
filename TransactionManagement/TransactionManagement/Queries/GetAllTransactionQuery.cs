using MediatR;
using TransactionManagement.Model.Entities;

namespace TransactionManagement.Queries
{
    public record GetAllTransactionQuery() : IRequest<IEnumerable<TransactRecord>>;
}
