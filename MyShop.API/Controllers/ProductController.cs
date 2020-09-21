using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.API.Controllers.Base;
using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Services;

namespace MyShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : DefaultApiController<ProductAddApiModel, ProductGetFullApiModel, ProductGetMinApiModel, Product, string, ProductSortingEnum>
    {
        public ProductController(IProductService service, IDataMapper dataMapper) : base(service, dataMapper)
        {
        }
    }
}