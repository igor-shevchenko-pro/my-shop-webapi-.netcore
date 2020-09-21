using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services.UserAccount
{
    public interface IRoleService : IBaseService<RoleAddApiModel, RoleGetFullApiModel, RoleGetMinApiModel, Role, string, RoleSortingEnum>
    {
        Task<PaginationResponseApiModel<IBaseApiModel<string>, RoleSortingEnum>> GetAllWithNestedManagersAsync(int start,
            int count, List<RoleSortingEnum> sorting, string query = null);
    }
}
