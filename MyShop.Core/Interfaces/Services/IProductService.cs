using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Services.Base;

namespace MyShop.Core.Interfaces.Services
{
    public interface IProductService : IBaseService<ProductAddApiModel, ProductGetFullApiModel, ProductGetMinApiModel, Product, string, ProductSortingEnum>
    {
    }
}
