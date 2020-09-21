using MyShop.ApiModels;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories.ManyToMany
{
    public class CategoryProductRepository : BaseRepository<CategoryProduct, int, EntitySortingEnum>, ICategoryProductRepository
    {
        public CategoryProductRepository(IGenericRepository<CategoryProduct, int> repository) : base(repository)
        {
        }
    }
}