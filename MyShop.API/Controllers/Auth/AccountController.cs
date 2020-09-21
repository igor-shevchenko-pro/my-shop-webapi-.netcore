using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Controllers.Base;
using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Auth;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Services.Auth;
using MyShop.Core.Interfaces.Services.UserAccount;

namespace MyShop.API.Controllers.Auth
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _service;
        private readonly IUserService _userService;
        private readonly IPasswordManager _passwordManager;

        public AccountController(IAccountService service, IUserService userService, IPasswordManager passwordManager) 
        {
            _service = service;
            _userService = userService;
            _passwordManager = passwordManager;
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult<SignInApiModel>> Registration(RegistrationApiModel model)
        {
            var user = await _service.RegistrationAsync(model.Login, model.Password, model.ContactType, model.UserProfile ?? new UserProfileAddApiModel());
            var signIn = await _service.GenerateSignInResponseAsync(user);

            return SuccessResult(signIn);
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<ActionResult<SignInApiModel>> Authentication(LoginApiModel model)
        {
            var user = await _service.AuthenticationAsync(model.Login, model.Password, model.ContactType);
            var signIn = await _service.GenerateSignInResponseAsync(user);

            return SuccessResult(signIn);
        }

        [AllowAnonymous]
        [HttpGet("anonymous")]
        public async Task<ActionResult<SignInApiModel>> GetAnonymous()
        {
            var user = await _service.AnonymousAsync();
            var signIn = await _service.GenerateSignInResponseAsync(user);

            return SuccessResult(signIn);
        }

        [AllowAnonymous]
        [HttpPost("recovery_password_request")]
        public async Task<ActionResult<SuccessResponseApiModel>> RecoveryPasswordRequest(RecoveryPasswordRequestApiModel model)
        {
            await _passwordManager.RecoveryPasswordRequestAsync(model.Contact, model.ContactType, model.FrontClientType);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }

        [AllowAnonymous]
        [HttpPost("recovery_password")]
        public async Task<ActionResult<SuccessResponseApiModel>> RecoveryPassword(RecoveryPasswordApiModel model)
        {
            await _passwordManager.RecoveryPasswordAsync(model.Contact, model.Code, model.NewPassword, model.ContactType);
            return SuccessResult(new SuccessResponseApiModel() { Response = "success" });
        }

        [HttpPost("change_password")]
        public async Task<ActionResult<SuccessResponseApiModel>> Change(ChangePasswordApiModel model)
        {
            var userId = GetUserId();
            await _passwordManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            return SuccessResult(new SuccessResponseApiModel() { Response = "success", Id = userId });
        }

        [HttpPost("refresh_token")]
        public async Task<ActionResult<SignInApiModel>> RefreshToken(RefreshTokenApiModel model)
        {
            var userId = GetUserId();
            var user = await _userService.GetAsync(userId, TypeModelResponseEnum.GetFullApiModel);
            var signIn = await _service.GenerateSignInResponseAsync(user as UserGetFullApiModel, model.RefreshToken);

            return SuccessResult(signIn);
        }
    }
}