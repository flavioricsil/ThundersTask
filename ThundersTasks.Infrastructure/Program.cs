using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThundersTasks.Infrastructure.Data;

namespace ThundersTasks.Infrastructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = configurationBuilder.Build();

            var _stringConexaoBanco = _configuration.GetConnectionString("DefaultConnection");

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                    optionsBuilder.UseSqlServer(_stringConexaoBanco);
                    optionsBuilder.EnableSensitiveDataLogging();
                    services.AddScoped<ApplicationDbContext>(s => new ApplicationDbContext(optionsBuilder.Options));
                }).Build();
        }
    }
}