using MediatR;
using TransactionManagement.Model;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Queries
{
    public record GetFilterTransactionQuery(FilterTransactionCsvRequest filterRequest) : IRequest<IEnumerable<TransactRecord>>;
}
