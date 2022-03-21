using DecoratorPattern.Decos;
using DecoratorPattern.Models;
using DecoratorPattern.Services;
using DecoratorPattern.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientRequester<IRequestModel, IResponseModel> _genericRequester;

        public WeatherForecastController(ILoggerFactory loggerFactory,
            ILogger<WeatherForecastController> logger,
            IHttpClientRequester<IRequestModel, IResponseModel> genericRequester)
        {
            this._loggerFactory = loggerFactory;
            _logger = logger;
            //this._genericRequester = genericRequester;
            this._genericRequester = new HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>(genericRequester, _loggerFactory.CreateLogger<HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>>());
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {

            var result = await _genericRequester.SendHttpClientRequest<IRequestModel, CurrencyModel>(null, "", HttpMethod.Get);

            return Ok(result);
        }
    }
}