using MediatR;
using TransactionManagement.Database;
using TransactionManagement.Model;
using TransactionManagement.Queries;
using TransactionManagement.Services;

namespace TransactionManagement.Handlers
{
    public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, IEnumerable<TransactRecord>>
    {
        private readonly AdoTransactionDbContext _dbContext;
        private readonly ICSVService _csvService;

        public GetAllTransactionHandler(AdoTransactionDbContext dbContext, ICSVService csvService)
        {
            _dbContext = dbContext;
            _csvService = csvService;
        }

        public async Task<IEnumerable<TransactRecord>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TransactRecord> transactRecords = await _dbContext.GetAllTransactions();

            //var transactByte = _csvService.CreateCSV<TransactRecord>(transactRecords);
            return transactRecords;
        }
    }
}
