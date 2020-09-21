using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;

namespace MyShop.BL.Services
{
    public class GenderService : BaseService<GenderAddApiModel, GenderGetFullApiModel, GenderGetMinApiModel, Gender, int, EntitySortingEnum>, IGenderService
    {
        public GenderService(IGenderRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
        }
    }
}