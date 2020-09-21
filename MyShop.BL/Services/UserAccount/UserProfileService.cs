using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.Core.Interfaces.Services.UserAccount;

namespace MyShop.BL.Services.UserAccount
{
    public class UserProfileService : BaseService<UserProfileAddApiModel, UserProfileGetFullApiModel, UserProfileGetMinApiModel, UserProfile, string, EntitySortingEnum>, IUserProfileService
    {
        public UserProfileService(IUserProfileRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
        }
    }
}