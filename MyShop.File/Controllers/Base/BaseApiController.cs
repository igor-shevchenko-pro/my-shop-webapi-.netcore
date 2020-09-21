using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Helpers;

namespace MyShop.File.Controllers.Base
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult<string> SuccessResult()
        {
            return Ok("Success");
        }

        protected ActionResult<T> SuccessResult<T>(T model)
        {
            return Ok(model);
        }

        private ErrorResponceApiModel<T> ResponseModelBuild<T>(T model)
        {
            ErrorResponceApiModel<T> result = new ErrorResponceApiModel<T>()
            {
                Errors = new List<string>(),
            };
            return result;
        }

        protected virtual string GetUserId()
        {
            return this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        protected int GetLanguageId()
        {
            int langId = 1;

            if (HttpContext.Request.Headers.ContainsKey("Accept-Language"))
            {
                var lang = HttpContext.Request.Headers["Accept-Language"].ToString();
                langId = LanguageHelper.Current.GetIdBySymbol(lang);
            }
            return langId;
        }
    }
}