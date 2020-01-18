using AirplaneServices.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirplaneServices.Infra.Config
{
    public static class ConfigDbConn
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //var environmentString = Environment.GetEnvironmentVariable("AzureDevConnection");
            var connString = @"Data Source=DESKTOP-OUM5KHF\SQLEXPRESS;Initial Catalog=Airplane;Integrated Security=True";

            services.AddDbContext<ContextBase>(options =>
                options.UseSqlServer(connString));
        }
    }
}
