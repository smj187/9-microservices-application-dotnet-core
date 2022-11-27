using BuildingBlocks.Multitenancy.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Multitenancy.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnvironmentService EnvService { get; }
        public string CurrentDirectory { get; set; }

        public ConfigurationService(IEnvironmentService envService, string currentDirecotry)
        {
            EnvService = envService;
            CurrentDirectory = currentDirecotry;
        }

        public IConfiguration GetConfiguration()
        {
            CurrentDirectory ??= Directory.GetCurrentDirectory();
            return new ConfigurationBuilder()
                .SetBasePath(CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnvService.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
