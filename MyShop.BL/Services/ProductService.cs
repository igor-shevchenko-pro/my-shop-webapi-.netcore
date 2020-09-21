using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;

namespace MyShop.BL.Services
{
    public class ProductService : BaseService<ProductAddApiModel, ProductGetFullApiModel, ProductGetMinApiModel, Product, string, ProductSortingEnum>, IProductService
    {
        public ProductService(IProductRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
        }
    }
}
