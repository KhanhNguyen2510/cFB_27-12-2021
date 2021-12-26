using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cFB.Data.EFs
{
    public class cFBDbContextFactory : IDesignTimeDbContextFactory<cFBDbContext>
    {
        public cFBDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<cFBDbContext>();

            optionsBuilder.UseSqlServer(connectionString);
            return new cFBDbContext(optionsBuilder.Options);
        }
    }
}
