using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Moq;
using System;

namespace AirplaneServices.WebAPI.Tests.Unit
{
    internal class AutoMoqServiceResolver : ISubDependencyResolver
    {
        private readonly IKernel kernel;

        public AutoMoqServiceResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return dependency.TargetType.IsInterface;
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            Type mock = typeof(Mock<>).MakeGenericType(dependency.TargetType);
            return ((Mock)kernel.Resolve(mock)).Object;
        }
    }

    public class BaseInstaller<TClass> : IWindsorInstaller
    {
        public void Install(
            IWindsorContainer container,
            IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new AutoMoqServiceResolver(container.Kernel));
            _ = container.Register(Component.For(typeof(Mock<>)));

            container.Register(Classes
                .FromAssemblyContaining<TClass>()
                .Pick()
                .WithServiceSelf()
                .LifestyleTransient());
        }
    }
}