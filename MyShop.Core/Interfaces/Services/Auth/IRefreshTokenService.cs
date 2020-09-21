using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services.Auth
{
    public interface IRefreshTokenService
    {
        Task<string> CreateTokenAsync(string userId);
        Task<string> UpdateTokenAsync(string userId, string token);
    }
}
