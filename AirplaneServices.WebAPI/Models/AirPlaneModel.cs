using System;

namespace AirplaneServices.WebAPI.Models
{
    public class AirPlaneModel
    {
        public AirPlaneModel(AirPlaneAddModel airPlane)
        {
            this.Id = Guid.NewGuid();
            this.Code = airPlane.Code;
            this.Model = null;
            this.CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public AirPlaneModelModel Model { get; set; }

        public int NumberOfPassengers { get; set; }

        public DateTime CreationDate { get; set; }
    }
}