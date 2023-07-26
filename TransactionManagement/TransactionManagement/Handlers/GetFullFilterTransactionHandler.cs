using MediatR;
using TransactionManagement.Database;
using TransactionManagement.Model;
using TransactionManagement.Queries;

namespace TransactionManagement.Handlers
{
    public class GetFullFilterTransactionHandler : IRequestHandler<GetFullFilterTransactionQuery, IEnumerable<TransactRecord>>
    {
        private readonly AdoTransactionDbContext _dbContext;

        public GetFullFilterTransactionHandler(AdoTransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactRecord>> Handle(GetFullFilterTransactionQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TransactRecord> transactRecords = await _dbContext.GetFullFilterTransactions(request.filterTransactionRequest);
                
            return transactRecords;
        }
    }
}
