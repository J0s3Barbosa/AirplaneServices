using AirplaneServices.Application.Interfaces;
using AirplaneServices.Application.Logic;
using AirplaneServices.Domain.Entities;
using Castle.Windsor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AirplaneServices.WebAPI.Tests.Unit
{
    public class AirPlaneControllerTests
    {
        private readonly IWindsorContainer _container;
        private readonly IAirPlaneLogic _AirPlaneLogic;

        public AirPlaneControllerTests()
        {
            _container = new WindsorContainer();
            _container.Install(new BaseInstaller<AirPlaneLogic>());
            _AirPlaneLogic = _container.Resolve<AirPlaneLogic>();
        }

        [Fact(DisplayName = "#00 - Cenário: ")]
        [Trait("Category", "Fail")]
        public void IAirPlaneLogic_Test()
        {
            Random randomNumber = new Random();

            var airPlaneModel = new Domain.Entities.AirPlaneModel
            {
                Id = new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"),
                Name = "Airbus A300B1"
            };

            var airPlane = new AirPlane
            {
                Id = Guid.NewGuid(),
                Code = randomNumber.Next(1, 90).ToString(),
                Model = airPlaneModel,
                CreationDate = DateTime.Now,
                NumberOfPassengers = randomNumber.Next(1, 300)
            };

            var airPlanes = _AirPlaneLogic.List();

            // Assert
            var result = airPlanes.Should().BeOfType<List<AirPlane>>().Subject;
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
        }
    }
}