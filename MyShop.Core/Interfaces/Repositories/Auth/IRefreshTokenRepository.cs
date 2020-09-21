using MyShop.ApiModels;
using MyShop.Core.Entities.Auth;
using MyShop.Core.Interfaces.Repositories.Base;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Repositories.Auth
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken, string, EntitySortingEnum>
    {
        Task<string> CreateTokenAsync(string userId);
        Task<string> UpdateTokenAsync(string userId, string token);
    }
}
