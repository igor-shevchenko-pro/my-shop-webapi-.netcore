using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Controllers.Base;
using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Request;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Services.UserAccount;
using System.Threading.Tasks;

namespace MyShop.API.Controllers.UserAccount
{
    [Authorize]
    [Route("api/[controller]")]
    public class RoleController : DefaultApiController<RoleAddApiModel, RoleGetFullApiModel, RoleGetMinApiModel, Role, string, RoleSortingEnum>
    {
        new IRoleService _service;

        public RoleController(IRoleService service, IDataMapper dataMapper) : base(service, dataMapper)
        {
            _service = service;
        }

        // Get all roles with nested managers. Without any other types of users
        [HttpPost("get_all_with_nested_managers")]
        public virtual async Task<ActionResult<PaginationResponseApiModel<IBaseApiModel<string>, RoleSortingEnum>>> GetRolesWithNestedManagers(
            SortedEntitiesRequestApiModel<RoleSortingEnum> model)
        {
            var models = await _service.GetAllWithNestedManagersAsync(model.Start, model.Count, model.Sortings, model.Query);

            return SuccessResult(models);
        }
    }
}