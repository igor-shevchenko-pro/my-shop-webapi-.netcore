using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MyShop.Core.Configurations
{
    public class AuthJwtConfig
    {
        private static readonly AuthJwtConfig _instance = new AuthJwtConfig();
        public static AuthJwtConfig Current => _instance;
        public string SecretKey => "mysupersecret_secretkey!123";

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public string SigningAlgorithm => SecurityAlgorithms.HmacSha256;

        public string Issuer => "MainAuthServer";
        public string Audience => "http://127.0.0.1:17181";

        private int LifetimeMinutes => 1440;
        public TimeSpan Lifetime => TimeSpan.FromMinutes(LifetimeMinutes);

        private AuthJwtConfig()
        {
        }
    }
}
