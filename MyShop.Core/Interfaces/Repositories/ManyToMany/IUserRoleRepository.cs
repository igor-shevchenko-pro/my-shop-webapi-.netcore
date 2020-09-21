using MyShop.ApiModels;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Repositories.ManyToMany
{
    public interface IUserRoleRepository : IBaseRepository<UserRole, string, EntitySortingEnum>
    {
        Task<IEnumerable<UserRole>> AddRolesAsync(string userId, params string[] roleIds);
        Task RemoveRolesAsync(string userId, params string[] roleIds);
        Task RemoveAllUserRolesAsync(string userId);
    }
}