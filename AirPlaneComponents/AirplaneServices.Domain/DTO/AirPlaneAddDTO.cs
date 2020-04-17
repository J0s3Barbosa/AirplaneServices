namespace AirplaneServices.Domain.DTO
{
    public class AirPlaneAddDTO
    {
        public string Code { get; set; }

        public AirPlaneModelDTO Model { get; set; }

        public int NumberOfPassengers { get; set; }
    }
}