using MediatR;
using TransactionManagement.Database;
using TransactionManagement.Model;
using TransactionManagement.Queries;

namespace TransactionManagement.Handlers
{
    public class GetFilterTransactionHandler : IRequestHandler<GetFilterTransactionQuery, IEnumerable<TransactRecord>>
    {
        private readonly AdoTransactionDbContext _dbContext;

        public GetFilterTransactionHandler(AdoTransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactRecord>> Handle(GetFilterTransactionQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TransactRecord> transactRecords = await _dbContext.GetFilterTransactions(request.filterRequest);

            return transactRecords;
        }
    }
}
