using Microsoft.Extensions.Configuration;
using MyShop.Core.Interfaces.Configurations.Base;

namespace MyShop.DL.PostgreSql
{
    public class PostgreSqlSettings : IDatabaseSettings
    {
        protected readonly string _connectionString;

        public PostgreSqlSettings()
        {
            _connectionString = "Host=localhost;Port=5432;Database=myshop_dev;Username=postgres;Password=Wego123!";
        }

        public PostgreSqlSettings(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
