using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;

namespace MyShop.BL.Services
{
    public class LanguageService : BaseService<LanguageAddApiModel, LanguageGetFullApiModel, LanguageGetMinApiModel, Language, int, EntitySortingEnum>, ILanguageService
    {
        public LanguageService(ILanguageRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
        }
    }
}