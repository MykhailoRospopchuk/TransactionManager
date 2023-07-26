using MediatR;
using TransactionManagement.Commands;
using TransactionManagement.Database;

namespace TransactionManagement.Handlers
{
    public class AddTransactionHandler : IRequestHandler<AddTransactionCommand>
    {
        private readonly AdoTransactionDbContext _dbContext;

        public AddTransactionHandler(AdoTransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.AddTransaction(request.transactRecord);
        }
    }
}
