using MediatR;
using TransactionManagement.Commands;
using TransactionManagement.Database;
using TransactionManagement.Model.Consts;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.ExceptionModel;

namespace TransactionManagement.Handlers
{
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly TransactionDbContext _dbContext;

        public UpdateTransactionHandler(TransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            TransactRecord record = await _dbContext.Transactions.FindAsync(request.updateTransaction.Id);

            if (record is null)
            {
                throw new BadRequestException(ConstantError.GetErrorForException(nameof(TransactRecord), request.updateTransaction.Id));
            }

            record.Status = request.updateTransaction.Status;

            await _dbContext.SaveChangesAsync();
        }
    }
}
