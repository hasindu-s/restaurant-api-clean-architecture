using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Getting weather forecasts");
            return _weatherForecastService.Get();
        }

        [HttpGet]
        [Route("test/{value}")]
        public IActionResult Test([FromRoute] string value, [FromQuery] string max)
        {
            return NotFound($"Test successful with value: {value}, max: {max}");
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult GenerateWeatherForecasts([FromQuery] int numOfResults, [FromQuery] int min, [FromQuery] int max)
        {
            if (numOfResults <= 0 || min >= max)
            {
                return BadRequest("Invalid parameters for weather forecast generation.");
            }

            var forecasts = _weatherForecastService.Generate(numOfResults, min, max);
            return Ok(forecasts);
        }
    }
}
