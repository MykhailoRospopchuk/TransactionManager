using Microsoft.EntityFrameworkCore;
using TransactionManagement.Database;
using TransactionManagement.Model.Consts;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.ExceptionModel;
using TransactionManagement.Model.RequestModel;
using TransactionManagement.Services.Interface;

namespace TransactionManagement.Services
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private readonly TransactionDbContext _context;

        public AuthenticatorService(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password);

            if (user == null)
            {
                throw new NotFoundException
                    (ConstantError.GetCredentialsErrorExceptionMessage
                        (nameof(User), loginRequest.Email, loginRequest.Password));
            }

            return user;
        }
    }
}
