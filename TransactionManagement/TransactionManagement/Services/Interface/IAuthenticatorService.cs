using TransactionManagement.Model.Entities;
using TransactionManagement.Model.RequestModel;

namespace TransactionManagement.Services.Interface
{
    public interface IAuthenticatorService
    {
        Task<User> AuthenticateAsync(LoginRequest loginRequest);
    }
}
