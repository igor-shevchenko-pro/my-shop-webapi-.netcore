using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Services.Base;

namespace MyShop.Core.Interfaces.Services
{
    public interface IGenderService : IBaseService<GenderAddApiModel, GenderGetFullApiModel, GenderGetMinApiModel, Gender, int, EntitySortingEnum>
    {
    }
}