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

        public AirPlaneLogic(IAirPlane iAirPlane)
        {
            this._IAirPlane = iAirPlane;
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

        public AirPlane GetEntity(Guid id)
        {
            return this._IAirPlane.GetEntity(id);
        }

        public List<AirPlane> List()
        {
            return this._IAirPlane.List();
        }

        public List<AirPlane> List(string code, string model, short? numberOfPassengers, DateTime? creationDate)
        {
            IEnumerable<AirPlane> airPlanes = this._IAirPlane.List();

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

            var resource = this._IAirPlane.GetEntity(identifier);
            if (resource == null) return result.ResultError("Resource not found!");

            resource.Code = Entity.Code;
            resource.ModelId = Entity.ModelId;
            resource.NumberOfPassengers = Entity.NumberOfPassengers;

            if (this.Update(resource) <= 0) return result.ResultError("The resource was not updated!");
            else return result.ResultResponse(resource);
        }
    }

}
