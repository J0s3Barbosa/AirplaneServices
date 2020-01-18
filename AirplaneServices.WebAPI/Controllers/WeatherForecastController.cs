using AirplaneServices.Application.Extensions;
using AirplaneServices.Application.Logic;
using AirplaneServices.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AirplaneServices.WebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerGroup("Weather Forecast Api")]
    [ApiController, Route("api/v{version:apiVersion}/[controller]"), Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastMicroservice weatherForecastMicroservice;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            var userManagementServiceUrl = _configuration["Microservices:LearningHubManagement"];
            weatherForecastMicroservice = string.IsNullOrEmpty(userManagementServiceUrl) ? new WeatherForecastMicroservice() : new WeatherForecastMicroservice(userManagementServiceUrl);
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var questions = weatherForecastMicroservice.GetWeatherForecast();

            //var result = new List<WeatherForecast>();
            //foreach (var item in questions)
            //{
            //    result.Add((WeatherForecast)new WeatherForecast().InjectFrom(item));
            //}
            return questions;
        }
    }
}
