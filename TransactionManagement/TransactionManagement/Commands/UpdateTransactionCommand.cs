using MediatR;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Commands
{
    public record UpdateTransactionCommand(UpdateTransactionRequest updateTransaction) : IRequest;

}
