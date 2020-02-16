using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AirplaneServices.WebAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {

            BuildWebHost(args).Run();

        }
        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.AddCommandLine(args)
.Build();

            var host = new WebHostBuilder()
       .UseConfiguration(config)
       .UseKestrel()
       .UseContentRoot(Directory.GetCurrentDirectory())
       .UseStartup<Startup>()
.UseUrls("https://localhost:5000")
       .Build();
            host.Run();

            return WebHost.CreateDefaultBuilder(args)
    .UseConfiguration(config)
    .UseStartup<Startup>()
    .Build();


        }


    }
}