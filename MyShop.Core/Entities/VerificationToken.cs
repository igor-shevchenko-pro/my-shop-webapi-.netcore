using MyShop.ApiModels;
using MyShop.Core.Entities.Base;

namespace MyShop.Core.Entities
{
    public class VerificationToken : BaseEntity<string>
    {
        public string Contact { get; set; }
        public string Token { get; set; }
        public VerificationTokenTypeEnum TokenType { get; set; }
    }
}
