using MyShop.Core.Entities.Auth;
using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.ManyToMany;
using System.Collections.Generic;

namespace MyShop.Core.Entities.UserAccount
{
    public class User : BaseEntity<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }

        public string UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // ManyToMany
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
