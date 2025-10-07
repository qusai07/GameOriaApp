using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GameOria.Infrastructure.Data
{
    public class GameOriaDbContextFactory : IDesignTimeDbContextFactory<GameOriaDbContext>
    {
        public GameOriaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameOriaDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = "Server=DESKTOP-N0VO2G5\\SQL22;User Id=Qusai;Password=Qusai;Database=GameOria;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);

            return new GameOriaDbContext(optionsBuilder.Options);
        }
    }
}
