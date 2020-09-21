using MyShop.ApiModels;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Repositories.Base;

namespace MyShop.Core.Interfaces.Repositories.UserAccount
{
    public interface IRoleRepository : IBaseRepository<Role, string, RoleSortingEnum>
    {
    }
}