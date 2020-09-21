using System;
using System.Threading.Tasks;
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

namespace MyShop.API.Controllers.UserAccount
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : DefaultApiController<UserAddApiModel, UserGetFullApiModel, UserGetMinApiModel, User, string, UserSortingEnum>
    {
        new IUserService _service;

        public UserController(IUserService service, IDataMapper dataMapper) : base(service, dataMapper)
        {
            _service = service;
        }

        [HttpGet("get_current_user")]
        public async Task<ActionResult<IBaseApiModel<string>>> GetCurrentUser(TypeModelResponseEnum modelResponseType)
        {
            var userId = GetUserId();
            var user = await _service.GetAsync(userId, modelResponseType);

            return SuccessResult(user);
        }

        [HttpPost("get_managers")]
        public async Task<ActionResult<PaginationResponseApiModel<IBaseApiModel<string>, UserSortingEnum>>> GetManagers(SortedEntitiesRequestApiModel<UserSortingEnum> model)
        {
            int langId = GetLanguageId();
            var result = await _service.GetManagersAsync(model.Start, model.Count, model.Sortings, model.ModelResponseType, langId);

            return SuccessResult(result);
        }

        [HttpPut("update/{id}")]
        public override async Task<ActionResult<SuccessResponseApiModel>> Update(string id, [FromBody] UserAddApiModel model)
        {
            model.Id = model.Id ?? id;
            model.UserProfileId = model.Id;
            if (model.UserProfile.FileEntityId == null) model.UserProfile.FileEntityId = "default-user";

            await _service.UpdateAsync(model);

            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }

        [HttpPut("set_default_photo/{id}")]
        public async Task<ActionResult<SuccessResponseApiModel>> SetDefaultPhoto(string id)
        {
            await _service.SetDefaultPhotoAsync(id);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }


        #region NonAction
        [NonAction]
        public override Task<ActionResult<PaginationResponseApiModel<IBaseApiModel<string>, UserSortingEnum>>> GetByUser(SortedEntitiesByUserRequestApiModel<UserSortingEnum> model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}