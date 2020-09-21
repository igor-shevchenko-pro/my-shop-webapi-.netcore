using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.ManyToMany;
using System.Collections.Generic;

namespace MyShop.Core.Entities.UserAccount
{
    public class Role : BaseManualEntity<string>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}