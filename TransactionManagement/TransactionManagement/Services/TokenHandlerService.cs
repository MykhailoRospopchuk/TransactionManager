using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransactionManagement.Database;
using TransactionManagement.Model.Consts;
using TransactionManagement.Model.Entities;
using TransactionManagement.Model.ExceptionModel;
using TransactionManagement.Model.RequestModel;
using TransactionManagement.Services.Interface;

namespace TransactionManagement.Services
{
    public class TokenHandlerService : ITokenHandlerService
    {
        private readonly IConfiguration _configuration;
        private readonly TransactionDbContext _context;

        public TokenHandlerService(IConfiguration configuration, TransactionDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            // Create Claims
            user.UserRefreshToken ??= new UserRefreshToken
            {
                Id = 0,
                Token = null,
                Expires = default,
                UserId = 0,
                User = null
            };

            var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("refreshTokenId", user.UserRefreshToken.Id.ToString()),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Email, user.Email),
            new("role", user.Role.RoleName.ToString())
        };

            // Token creation
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<RefreshResponse> Refresh(string expiredToken)
        {
            UserRefreshToken refreshToken;
            int userId;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(expiredToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                userId = int.Parse(jwtToken!.Claims
                    .FirstOrDefault(x => x.Type == "id")!.Value);

                refreshToken = new UserRefreshToken
                {
                    Token = Guid.NewGuid().ToString(),
                    Expires = DateTime.Now.AddDays(30),
                    UserId = userId
                };

                var refreshTokenId = int.Parse(jwtToken!.Claims
                    .FirstOrDefault(x => x.Type == "refreshTokenId")!.Value);


                var tokenForUpdate = await _context.UserRefreshTokens.Where(x => x.Id == refreshTokenId).FirstOrDefaultAsync();

                switch (tokenForUpdate)
                {
                    case not null:
                        tokenForUpdate.Token = refreshToken.Token;
                        tokenForUpdate.Expires = refreshToken.Expires;
                        await _context.SaveChangesAsync();
                        break;
                    case null:
                        _context.UserRefreshTokens.Add(refreshToken);
                        await _context.SaveChangesAsync();
                        break;
                }
            }
            catch (Exception)
            {
                throw new BadRequestException("Invalid token!");
            }

            var user = await _context.Users.Where(x => x.Id == userId)
                .AsNoTracking()
                .Include(x => x.Role)
                .Include(x => x.UserRefreshToken)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new NotFoundException(ConstantError.GetErrorForException(nameof(User), userId));
            }

            user.UserRefreshToken = refreshToken;
            await _context.SaveChangesAsync();

            var userAfterUpdate = await _context.Users.Where(x => x.Id == userId)
                .AsNoTracking()
                .Include(x => x.Role)
                .Include(x => x.UserRefreshToken)
                .FirstOrDefaultAsync();
            var newToken = await CreateTokenAsync(userAfterUpdate);

            return new RefreshResponse
            {
                NewJwtToken = newToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
