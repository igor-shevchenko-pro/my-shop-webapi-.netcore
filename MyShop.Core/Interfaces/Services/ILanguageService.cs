using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Services.Base;

namespace MyShop.Core.Interfaces.Services
{
    public interface ILanguageService : IBaseService<LanguageAddApiModel, LanguageGetFullApiModel, LanguageGetMinApiModel, Language, int, EntitySortingEnum>
    {
    }
}