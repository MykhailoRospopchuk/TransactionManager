using TransactionManagement.Model.Entities;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Services.Interface
{
    public interface ITokenHandlerService
    {
        Task<string> CreateTokenAsync(User user);
        Task<RefreshResponse> Refresh(string expiredToken);
    }
}
