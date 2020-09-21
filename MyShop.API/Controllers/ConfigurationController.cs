using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Controllers.Base;
using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Interfaces.Services;

namespace MyShop.API.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : BaseApiController
    {
        private readonly ILanguageService _languageService;
        private readonly IGenderService _genderService;

        public ConfigurationController(ILanguageService languageService, IGenderService genderService)
        {
            _languageService = languageService;
            _genderService = genderService;
        }

        [HttpGet("dictionaries")]
        public async Task<ActionResult<DictionariesApiModel<int>>> Dictionaries()
        {
            var langId = GetLanguageId();
            var start = 0;
            var count = 1000;
            var sorting = new List<EntitySortingEnum>() { EntitySortingEnum.ByCreateDesc };
            var modelResponseType = TypeModelResponseEnum.GetMinApiModel; 

            DictionariesApiModel<int> result = new DictionariesApiModel<int>();

            var languages = await _languageService.GetAsync(start, count, sorting, modelResponseType, langId);
            var gender = await _genderService.GetAsync(start, count, sorting, modelResponseType, langId);

            result.Languages = languages.Models.ToList();
            result.Genders = gender.Models.ToList();

            return SuccessResult(result);
        }
    }
}