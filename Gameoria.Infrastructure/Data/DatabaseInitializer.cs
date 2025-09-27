using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameOria.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GameOriaDbContext>();

            var scriptFolder = Path.Combine(AppContext.BaseDirectory, "SqlScripts");
            var scripts = Directory.GetFiles(scriptFolder, "*.sql");

            foreach (var script in scripts)
            {
                var sql = File.ReadAllText(script);
                context.Database.ExecuteSqlRaw(sql);
            }
        }
    }
}
