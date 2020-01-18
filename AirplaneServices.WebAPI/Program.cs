using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AirplaneServices.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
            // CreateHostBuilder(args).Build().MigrateDbContext<ContextBase>(context => { }).Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
