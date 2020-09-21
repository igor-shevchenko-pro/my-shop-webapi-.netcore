using MyShop.ApiModels;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Helpers;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.DL.Repositories.ManyToMany
{
    public class UserRoleRepository : BaseRepository<UserRole, string, EntitySortingEnum>, IUserRoleRepository
    {
        public UserRoleRepository(IGenericRepository<UserRole, string> repository) : base(repository)
        {
        }

        public async Task<IEnumerable<UserRole>> AddRolesAsync(string userId, params string[] roleIds)
        {
            foreach (var roleId in roleIds)
            {
                var key = await _repository.AnyAsync(x => x.Id == userId && x.RoleId == roleId);
                if (key == false)
                {
                    await _repository.CreateAsync(new UserRole()
                    {
                        Id = userId,
                        RoleId = roleId
                    });
                }
            }

            return roleIds.Select(x => new UserRole()
            {
                Id = userId,
                RoleId = x,
                Role = RoleHelper.Current.GetRole(x),
            });
        }

        public async Task RemoveAllUserRolesAsync(string userId)
        {
            var userRoles = await _repository.WhereAsync(x => x.Id == userId);
            if(userRoles != null && userRoles.ToList().Count > 0)
            {
                foreach(var item in userRoles.ToList())
                {
                    await _repository.DeleteAsync(item);
                }
            }
        }

        // DeleteAsync defenite roles
        public async Task RemoveRolesAsync(string userId, params string[] roleIds)
        {
            foreach (var role in roleIds)
            {
                var key = await _repository.AnyAsync(x => x.Id == userId && x.RoleId == role);
                if (key)
                {
                    await _repository.DeleteAsync(role);
                }
            }
        }
    }
}
