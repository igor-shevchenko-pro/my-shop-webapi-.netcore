using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.UserAccount;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities.ManyToMany
{
    public class UserRole : BaseEntity<string>
    {
        [ForeignKey("User")]
        public override string Id { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}