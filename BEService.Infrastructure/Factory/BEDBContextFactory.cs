using BEService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BEService.Infrastructure.Factory
{
    public class BEDBContextFactory : IDesignTimeDbContextFactory<BEDBContext>
    {
        public BEDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<BEDBContext>();
            var connectionString = configuration.GetConnectionString("Connection");
            builder.UseSqlServer(connectionString);
            return new BEDBContext(builder.Options);
        }
    }
}
