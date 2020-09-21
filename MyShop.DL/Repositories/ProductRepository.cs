using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories
{
    public class ProductRepository : BaseRepository<Product, string, ProductSortingEnum>, IProductRepository
    {
        public ProductRepository(IGenericRepository<Product, string> repository) : base(repository)
        {
        }
    }
}