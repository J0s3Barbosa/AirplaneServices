namespace AirplaneServices.WebAPI.Models
{
    public class AirPlaneAlterModel
    {
        public string Code { get; set; }

        public AirPlaneModelAddModel Model { get; set; }

        public int NumberOfPassengers { get; set; }
    }
}