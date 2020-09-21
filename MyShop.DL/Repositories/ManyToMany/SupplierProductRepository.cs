using MyShop.ApiModels;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories.ManyToMany
{
    public class SupplierProductRepository : BaseRepository<SupplierProduct, string, EntitySortingEnum>, ISupplierProductRepository
    {
        public SupplierProductRepository(IGenericRepository<SupplierProduct, string> repository) : base(repository)
        {
        }
    }
}
