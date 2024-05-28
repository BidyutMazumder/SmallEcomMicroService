using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {
        static string connectionString = new ConfigurationBuilder()
            .SetBasePath(Directory
            .GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build().GetConnectionString("Catalog.API");
        static string databaseName = new ConfigurationBuilder()
            .SetBasePath(Directory
            .GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build().GetConnectionString("Catalog.API");
        public CatalogDbContext(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }
    }
}
