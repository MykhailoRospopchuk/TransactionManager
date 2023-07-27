using MediatR;
using System.Reflection.Metadata;
using TransactionManagement.Database;
using TransactionManagement.Model.Consts;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.ExceptionModel;
using TransactionManagement.Queries;
using TransactionManagement.Services.Interface;

namespace TransactionManagement.Handlers
{
    public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, IEnumerable<TransactRecord>>
    {
        private readonly AdoTransactionDbContext _dbContext;

        public GetAllTransactionHandler(AdoTransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactRecord>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TransactRecord> transactRecords = await _dbContext.GetAllTransactions();

            return transactRecords;
        }
    }
}
