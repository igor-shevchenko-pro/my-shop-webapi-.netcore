using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MyShop.Core.Managers.Base
{
    public class ConfigurationBuilderManager
    {
        private static readonly ConfigurationBuilderManager _instance = new ConfigurationBuilderManager();
        public static ConfigurationBuilderManager Current => _instance;

        private IConfigurationRoot _configuration;
        private string _configFileName;

        public ConfigurationBuilderManager()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == EnvironmentName.Development)
                _configFileName = "appsettings.Development.json";
            if (environment == EnvironmentName.Production)
                _configFileName = "appsettings.Production.json";

            var configBuilder = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile(_configFileName, optional: true);

            _configuration = configBuilder.Build();
        }

        public string GetConfiguration(string section)
        {
            return _configuration.GetSection(section).Value;
        }
    }
}
