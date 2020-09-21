using MyShop.ApiModels;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories.UserAccount
{
    public class UserProfileRepository : BaseRepository<UserProfile, string, EntitySortingEnum>, IUserProfileRepository
    {
        public UserProfileRepository(IGenericRepository<UserProfile, string> repository) : base(repository)
        {
        }
    }
}