using MediatR;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Queries
{
    public record GetFilterTransactionQuery(FilterTransactionCsvRequest filterRequest) : IRequest<IEnumerable<TransactRecord>>;
}
