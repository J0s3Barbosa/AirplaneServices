using AirplaneServices.Application.Interfaces;
using AirplaneServices.Application.Logic;
using AirplaneServices.Infra.Context;
using AirplaneServices.Infra.Transactions;
using Microsoft.EntityFrameworkCore;
using Unity;
using Unity.Lifetime;

namespace AirplaneServices.IoC.Unity
{
    public static class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {

            container.RegisterType<DbContext, ContextBase>(new HierarchicalLifetimeManager());

            //UnitOfWork
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());


            container.RegisterType<IAirPlaneLogic, AirPlaneLogic>(new HierarchicalLifetimeManager());
            container.RegisterType<IAirPlaneModelLogic, AirPlaneModelLogic>(new HierarchicalLifetimeManager());



        }
    }

}
