using MyShop.ApiModels;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories.ManyToMany
{
    public class OrderProductRepository : BaseRepository<OrderProduct, string, EntitySortingEnum>, IOrderProductRepository
    {
        public OrderProductRepository(IGenericRepository<OrderProduct, string> repository) : base(repository)
        {
        }
    }
}