using AirplaneServices.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AirplaneServices.WebAPI.Extensions
{
    internal static class IWebHostExtensions
    {
        internal static IHost MigrateDbContext<TContext>(this IHost webHost, Action<TContext> seeder) where TContext : DbContext
        {
            Task.Run(delegate
            {
                using (var scope = webHost.Services.CreateScope())
                {
                    var context = new ContextBase();

                    try
                    {
                        context.Database.Migrate();
                        context.Dispose();
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
                        logger.LogError(ex, "An error occurred while migrating the database.");
                    }
                }
            });

            return webHost;
        }
    }

}
