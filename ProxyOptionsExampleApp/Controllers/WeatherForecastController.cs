using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProxyOptionsExampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly MyExampleHttpClient _apiClient;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            MyExampleHttpClient apiClient,
            ILogger<WeatherForecastController> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            string result = await _apiClient.CallTheService();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)] + result,
            })
            .ToArray();
        }
    }
}
