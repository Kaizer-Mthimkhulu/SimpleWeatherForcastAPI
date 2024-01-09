using Microsoft.AspNetCore.Mvc;

namespace SimpleWeatherForcastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Get a list of weather forecasts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /WeatherForecast
        /// </remarks>
        /// <returns>Returns a list of weather forecasts</returns>
        /// <response code="200">Returns the list of weather forecasts</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherForecast>))]
        public IEnumerable<WeatherForecast> Get()
        {
            var summaries = _configuration.GetSection("Weather:Summaries").Get<string[]>();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)],
            })
            .ToArray();
        }
    }
}