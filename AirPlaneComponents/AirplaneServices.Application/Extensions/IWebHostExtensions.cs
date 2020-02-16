using AirplaneServices.Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AirplaneServices.Application.Extensions
{

    internal static class IWebHostExtensions
    {
        internal static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext> seeder) where TContext : DbContext
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
