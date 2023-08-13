using Innoloft.Events.Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Events.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task RunMigrationsAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            var services = scope.ServiceProvider;
            try
            {
                var storageContext = services.GetRequiredService<EventsDbContext>();
                var pendingMigrations = await storageContext.Database.GetPendingMigrationsAsync();
                var count = pendingMigrations.Count();
                if (count > 0)
                {
                    logger.LogInformation($"found {count} pending migrations to apply. will proceed to apply them");
                    await storageContext.Database.MigrateAsync();
                    logger.LogInformation($"done applying pending migrations");
                }
                else
                {
                    logger.LogInformation($"no pending migrations found! :)");
                }

                //await  SeedDb(storageContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while performing migration.");
                throw;
            }
        }
    }
}