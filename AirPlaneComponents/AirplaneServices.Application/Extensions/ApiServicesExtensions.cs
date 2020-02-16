using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirplaneServices.Application.Extensions
{
    public static class ApiServicesExtensions
    {
        static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public static void AddExtentions(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            builder.SetCors(MyAllowSpecificOrigins);
            builder.SetDI();
            builder.SetApiVersion();
            builder.SetSwagger();

            //builder.AddServices();
        }

        public static void AddExtensions(this IApplicationBuilder app, IApiVersionDescriptionProvider versionProvider)
        {
            app.UseCors(MyAllowSpecificOrigins);
            app.SetSwagger(versionProvider);

        }

    }

}
