using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirplaneServices.Domain.Entities
{
    public class AirPlane
    {
        public AirPlane()
        {
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public Guid ModelId { get; set; }

        [ForeignKey("ModelId")]
        public virtual AirPlaneModel Model { get; set; }

        public int NumberOfPassengers { get; set; }

        public DateTime CreationDate { get; set; }
    }
}