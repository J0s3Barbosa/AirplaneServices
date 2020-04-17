using AirplaneServices.Application.Interfaces;
using AirplaneServices.Domain.Entities;
using AirplaneServices.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace AirplaneServices.Application.Logic
{
    public class AirPlaneModelLogic : IAirPlaneModelLogic
    {
        IAirPlaneModel _IAirPlaneModel;

        public AirPlaneModelLogic(IAirPlaneModel iAirPlaneModel)
        {
            this._IAirPlaneModel = iAirPlaneModel;
        }

        public int Add(AirPlaneModel Entity)
        {
            return this._IAirPlaneModel.Add(Entity);
        }

        public int Delete(AirPlaneModel Entity)
        {
            return this._IAirPlaneModel.Delete(Entity);
        }

        public AirPlaneModel GetEntity(Guid id)
        {
            return this._IAirPlaneModel.GetEntity(id);
        }

        public List<AirPlaneModel> List()
        {
            return this._IAirPlaneModel.List();
        }

        public int Update(AirPlaneModel Entity)
        {
            return this._IAirPlaneModel.Update(Entity);
        }
    }

}
