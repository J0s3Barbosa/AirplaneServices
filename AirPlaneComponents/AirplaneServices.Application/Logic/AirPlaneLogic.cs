using AirplaneServices.Application.Interfaces;
using AirplaneServices.Domain.Entities;
using AirplaneServices.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirplaneServices.Application.Logic
{
    public class AirPlaneLogic : IAirPlaneLogic
    {
        IAirPlane _IAirPlane;
        IAirPlaneModel _IAirPlaneModel;

        public AirPlaneLogic(IAirPlane iAirPlane,
            IAirPlaneModel iAirPlaneModel)
        {
            this._IAirPlane = iAirPlane;
            this._IAirPlaneModel = iAirPlaneModel;
        }


        public List<AirPlane> List()
        {
            List<AirPlane> aiplanes = _IAirPlane.List();
            List<AirPlaneModel> aiplanesModels = _IAirPlaneModel.List();

            var listOfAirplaines = aiplanes
        .Join(
            aiplanesModels,
            airplane => airplane.ModelId,
            aiplanesModel => aiplanesModel.Id,
            (airplane, aiplanesModel) => new AirPlane
            {
                Id = airplane.Id,
                Code = airplane.Code,
                ModelId = airplane.ModelId,
                NumberOfPassengers = airplane.NumberOfPassengers,
                CreationDate = airplane.CreationDate,
                Model = aiplanesModel

            })
        .ToList();


            return listOfAirplaines;
        }

        /// <summary>
        /// get AirPlane with AirPlaneModel associated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AirPlane GetEntity(Guid id)
        {
            AirPlane aiplane = _IAirPlane.GetEntity(id);
            AirPlaneModel aiplanesModel = _IAirPlaneModel.GetEntity(aiplane.ModelId);
            aiplane.Model = aiplanesModel;

            return aiplane;
        }

        /// <summary>
        /// get AirPlane and AirPlaneModel with param and fill AirPlane dependency
        /// </summary>
        /// <param name="airplaneId"></param>
        /// <param name="airplaneModelId"></param>
        /// <returns></returns>
        public AirPlane GetEntity(Guid airplaneId, Guid airplaneModelId)
        {
            AirPlane airplane = _IAirPlane.GetEntity(airplaneId);
            AirPlaneModel airplaneModel = _IAirPlaneModel.GetEntity(airplaneModelId);
            airplane.Model = airplaneModel;

            return airplane;
        }
        public List<AirPlane> List(string code, string model, short? numberOfPassengers, DateTime? creationDate)
        {
            IEnumerable<AirPlane> airPlanes = List();

            if (!string.IsNullOrEmpty(code))
                airPlanes = airPlanes.Where(x => x.Code.Contains(code, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(model))
                airPlanes = airPlanes.Where(x => x.Model.Name.Contains(model, StringComparison.OrdinalIgnoreCase));

            if (numberOfPassengers != null && numberOfPassengers >= 0)
                airPlanes = airPlanes.Where(x => x.NumberOfPassengers == numberOfPassengers);

            if (creationDate != null)
                airPlanes = airPlanes.Where(x => x.CreationDate == creationDate);

            return airPlanes.ToList();
        }

        public int Add(AirPlane Entity)
        {
            return this._IAirPlane.Add(Entity);
        }

        public Result<AirPlane> AddEntity(AirPlane Entity)
        {
            var result = new Result<AirPlane>();

            if (Entity.NumberOfPassengers > 300 || Entity.NumberOfPassengers < 1)
                return result.ResultError("Air Plane number of passengers is invalid!");

            if (this.List().Any(x => x.Code.Equals(Entity.Code, StringComparison.OrdinalIgnoreCase)))
                return result.ResultError("This code has already been registered!");

            Entity.Id = Guid.NewGuid();
            Entity.CreationDate = DateTime.Now;

            var save = this.Add(Entity);
            return (save > 0 ? result.ResultResponse(this.List().First(x => x.Code == Entity.Code)) : result.ResultError("Could not save data!"));
        }

        public int Delete(AirPlane Entity)
        {
            return this._IAirPlane.Delete(Entity);
        }

        public int? Delete(Guid id)
        {
            var entity = this.GetEntity(id);
            if (entity == null) return null;
            return this.Delete(entity);
        }


        public int Update(AirPlane Entity)
        {
            return this._IAirPlane.Update(Entity);
        }

        public Result<AirPlane> Update(Guid identifier, AirPlane Entity)
        {
            var result = new Result<AirPlane>();

            if (Entity.NumberOfPassengers > 300 || Entity.NumberOfPassengers < 1)
                return result.ResultError("Air Plane number of passengers is invalid!");

            if (this.List().Any(x => x.Code.Equals(Entity.Code, StringComparison.OrdinalIgnoreCase)))
                return result.ResultError("This code has already been registered!");

            var resource = GetEntity(identifier, Entity.ModelId);
            if (resource == null) return result.ResultError("Resource not found!");

            resource.Code = Entity.Code;
            resource.ModelId = Entity.ModelId;
            resource.Model = resource.Model;
            resource.NumberOfPassengers = Entity.NumberOfPassengers;

            if (this.Update(resource) <= 0) return result.ResultError("The resource was not updated!");
            else return result.ResultResponse(resource);
        }
    }

}

