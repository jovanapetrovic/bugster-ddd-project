using Bugster.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bugster.Extensions
{
    public static class MigrationManager
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<BugsterDbContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                }
                catch
                {
                    throw;
                }
            }

            return webHost;
        }
    }
}
