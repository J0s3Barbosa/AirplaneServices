using System;

namespace AirplaneServices.Domain.DTO
{
    public class AirPlaneDTO
    {
        public AirPlaneDTO(AirPlaneAddDTO airPlane)
        {
            this.Id = Guid.NewGuid();
            this.Code = airPlane.Code;
            this.Model = null;
            this.CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public AirPlaneModelDTO Model { get; set; }

        public int NumberOfPassengers { get; set; }

        public DateTime CreationDate { get; set; }
    }

}
