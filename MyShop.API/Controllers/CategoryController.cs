using Microsoft.AspNetCore.Mvc;
using MyShop.API.Controllers.Base;
using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Services;

namespace MyShop.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : DefaultApiController<CategoryAddApiModel, CategoryGetFullApiModel, CategoryGetMinApiModel, Category, int, CategorySortingEnum>
    {
        public CategoryController(ICategoryService service, IDataMapper dataMapper) : base(service, dataMapper)
        {
        }
    }
}