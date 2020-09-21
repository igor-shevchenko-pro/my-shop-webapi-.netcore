using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Services.Base;

namespace MyShop.Core.Interfaces.Services.UserAccount
{
    public interface IUserProfileService : IBaseService<UserProfileAddApiModel, UserProfileGetFullApiModel, UserProfileGetMinApiModel, UserProfile, string, EntitySortingEnum>
    {
    }
}