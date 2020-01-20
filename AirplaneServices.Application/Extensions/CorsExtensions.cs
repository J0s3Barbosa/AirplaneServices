using Microsoft.Extensions.DependencyInjection;

namespace AirplaneServices.Application.Extensions
{
    public static class CorsExtensions
    {

        public static void SetCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200"
                        )
                    .AllowAnyMethod();
                });


            });


        }


    }
}
