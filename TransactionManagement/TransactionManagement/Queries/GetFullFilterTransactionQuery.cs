using MediatR;
using TransactionManagement.Model;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Queries
{
    public record GetFullFilterTransactionQuery(FilterTransactionRequest filterTransactionRequest) : IRequest<IEnumerable<TransactRecord>>;
}
