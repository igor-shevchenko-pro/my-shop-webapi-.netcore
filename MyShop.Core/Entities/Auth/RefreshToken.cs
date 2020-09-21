using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.UserAccount;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities.Auth
{
    public class RefreshToken : BaseEntity<string>
    {
        [ForeignKey("User")] 
        public override string Id { get; set; }
        public virtual User User { get; set; }

        public string Token { get; set; }
        public string SecurityStamp { get; set; }
    }
}
