using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace AirplaneServices.Application.Extensions
{
    public static class ApiVersionExtensions
    {
        public static IServiceCollection SetApiVersion(this IServiceCollection builder)
        {
            builder.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            builder.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            return builder;
        }
    }

}
