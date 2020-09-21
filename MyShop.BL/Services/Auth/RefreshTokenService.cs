using MyShop.Core.Interfaces.Repositories.Auth;
using MyShop.Core.Interfaces.Services.Auth;
using System.Threading.Tasks;

namespace MyShop.BL.Services.Auth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> CreateTokenAsync(string userId)
        {
            return await _refreshTokenRepository.CreateTokenAsync(userId);
        }

        public async Task<string> UpdateTokenAsync(string userId, string token)
        {
            return await _refreshTokenRepository.UpdateTokenAsync(userId, token);
        }
    }
}
