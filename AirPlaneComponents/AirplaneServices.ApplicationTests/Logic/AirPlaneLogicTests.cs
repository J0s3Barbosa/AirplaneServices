using AirplaneServices.Application.Interfaces;
using AirplaneServices.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AirplaneServices.Application.Logic.Tests
{
    [TestClass()]
    public class AirPlaneLogicTests : AutoFixture.Fixture
    {
        private readonly IAirPlaneLogic _IAppAirPlaneLogic;

        public AirPlaneLogicTests(IAirPlaneLogic iAppAirPlaneLogic)
        {
            _IAppAirPlaneLogic = iAppAirPlaneLogic;
        }


        [TestMethod()]
        public void ListTest()
        {
            List<AirPlane> airPlanes = _IAppAirPlaneLogic.List();


            Assert.AreNotEqual(airPlanes.Count, 0);
        }
    }
}