namespace AirplaneServices.Application.Extensions
{
    public class ApiSettings
    {
        public string ApiName { get; set; }
        public int KeyboardTtlInSeconds { get; set; }
        public bool CacheActivated { get; set; }
        public int CacheTtlDefaultInDays { get; set; }
    }
}