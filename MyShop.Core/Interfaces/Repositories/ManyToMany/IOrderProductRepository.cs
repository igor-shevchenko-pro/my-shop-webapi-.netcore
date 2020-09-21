using MyShop.ApiModels;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Repositories.Base;

namespace MyShop.Core.Interfaces.Repositories.ManyToMany
{
    public interface IOrderProductRepository : IBaseRepository<OrderProduct, string, EntitySortingEnum>
    {
    }
}