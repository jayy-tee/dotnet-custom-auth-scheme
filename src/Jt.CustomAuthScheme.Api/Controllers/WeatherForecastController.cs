using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jt.CustomAuthScheme.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return GetWeatherForecasts();
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<WeatherForecast> GetAdminWeather()
        {
            return GetWeatherForecasts();
        }

        [HttpGet]
        [Route("superadmin")]
        [Authorize(Roles = "SuperAdmin")]
        public IEnumerable<WeatherForecast> GetSuperAdminWeather()
        {
            return GetWeatherForecasts();
        }

        private IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}