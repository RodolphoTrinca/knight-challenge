using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Knight.Infra.Context
{
    public class KnightContextFactory : IDesignTimeDbContextFactory<KnightDbContext>
    {
        public KnightDbContext CreateDbContext(string[] args)
        {
            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Knight.API"))
                .AddJsonFile("appsettings.local.json")
                .Build();

            var client = new MongoClient(config["KnightStoreDatabase:ConnectionString"]);
            var dataBase = client.GetDatabase(config["KnightStoreDatabase:DatabaseName"]);

            var optionsBuilder = new DbContextOptionsBuilder<KnightDbContext>();
            optionsBuilder.UseMongoDB(dataBase.Client, dataBase.DatabaseNamespace.DatabaseName);
            return new KnightDbContext(optionsBuilder.Options);
        }
    }
}
