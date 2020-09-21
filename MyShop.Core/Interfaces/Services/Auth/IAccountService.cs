using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Auth;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services.Auth
{
    public interface IAccountService
    {
        Task<UserGetFullApiModel> AuthenticationAsync(string login, string password, ContactTypeEnum contactType);
        Task<UserGetFullApiModel> RegistrationAsync(string login, string password, ContactTypeEnum contactType,
            UserProfileAddApiModel userProfile);

        Task<UserGetFullApiModel> AnonymousAsync();

        Task<SignInApiModel> GenerateSignInResponseAsync(UserGetFullApiModel userModel, string refreshToken = null);
    }
}
