using DecoratorPattern.Decos;
using DecoratorPattern.Models;
using DecoratorPattern.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHttpClientRequester<IRequestModel, IResponseModel> _httpClient;

        public WeatherForecastController(ILoggerFactory loggerFactory,
            IHttpClientRequester<IRequestModel, IResponseModel> genericRequester,
            IMemoryCache memoryCache)
        {
            this._loggerFactory = loggerFactory;
            var withLogger = new HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>(genericRequester, _loggerFactory.CreateLogger<HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>>());

            var withCache = new HttpClientRequesterCachingDecorator<IRequestModel, IResponseModel>(withLogger, memoryCache);
            _httpClient = withCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {

            var result = await _httpClient.SendHttpClientRequest<IRequestModel, CurrencyModel>(null, "", HttpMethod.Get);

            return Ok(result);
        }
    }
}