using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Entities.Base;
using System.Collections.Generic;

namespace MyShop.Core.Entities
{
    public class Order : BaseManualEntity<string>, IUserEntity
    {
        public string Description { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}