using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProductManagement.DAL
{
    public class ProductManagementContextFactory : IDesignTimeDbContextFactory<ProductManagementContext>
    {
        public ProductManagementContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ProductManagementContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ProductManagementContext(optionsBuilder.Options);
        }
    }
}
