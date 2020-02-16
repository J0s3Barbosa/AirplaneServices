using AirplaneServices.Application.Interfaces;
using AirplaneServices.Application.Logic;
using AirplaneServices.Domain.Interfaces;
using AirplaneServices.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AirplaneServices.Application.Extensions
{
    public static class MappingExtensions
    {
        public static void SetDI(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGeneric<>));
            services.AddSingleton<IAirPlane, RepositoryAirPlane>();
            services.AddSingleton<IAirPlaneModel, RepositoryAirPlaneModel>();

            services.AddSingleton<IAirPlaneLogic, AirPlaneLogic>();
            services.AddSingleton<IAirPlaneModelLogic, AirPlaneModelLogic>();


        }


    }
}
