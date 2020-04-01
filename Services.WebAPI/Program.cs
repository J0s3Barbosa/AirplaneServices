using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AirplaneServices.WebAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            BuildWebHost(args)
                .Run();


        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
.UseStartup<Startup>()
.UseConfiguration(Configuration)
.Build();

            return host;
        }



    }
}