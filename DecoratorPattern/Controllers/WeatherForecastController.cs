using DecoratorPattern.Models;
using DecoratorPattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpClientRequester<IRequestModel, IResponseModel> _httpClient;

        public WeatherForecastController(IHttpClientRequester<IRequestModel, IResponseModel> httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {

            var result = await _httpClient.SendHttpClientRequest<IRequestModel, CurrencyModel>(null, "", HttpMethod.Get);

            return Ok(result);
        }
    }
}