using AirplaneServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AirplaneServices.Application.Logic
{
    public class WeatherForecastMicroservice
    {
        private readonly string baseUSerManagementMicroserviceUrl = "";
        private readonly HttpClient _httpClient;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastMicroservice(string baseUerManagementServiceUrl = null)
        {
            if (!string.IsNullOrEmpty(baseUerManagementServiceUrl))
                baseUSerManagementMicroserviceUrl = baseUerManagementServiceUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
                .ToList();
        }
    }

}
