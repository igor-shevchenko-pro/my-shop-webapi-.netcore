using MyShop.ApiModels;
using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Repositories.UserAccount
{
    public interface IUserRepository : IBaseRepository<User, string, UserSortingEnum>
    {
        Task<CollectionOfEntities<User, UserSortingEnum>> GetManagersAsync(int start, int count, List<UserSortingEnum> sortings, int langId);
    }
}