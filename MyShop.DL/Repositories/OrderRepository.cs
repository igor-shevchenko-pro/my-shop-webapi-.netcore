using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories
{
    public class OrderRepository : BaseRepository<Order, string, OrderSortingEnum>, IOrderRepository
    {
        public OrderRepository(IGenericRepository<Order, string> repository) : base(repository)
        {
        }
    }
}