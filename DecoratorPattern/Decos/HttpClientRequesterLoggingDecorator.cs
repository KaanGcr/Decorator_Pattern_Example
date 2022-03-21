using DecoratorPattern.Models;
using DecoratorPattern.Services;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DecoratorPattern.Decos
{
    public class HttpClientRequesterLoggingDecorator<T, K> : IHttpClientRequester<T, K>
        where T : IRequestModel
        where K : IResponseModel
    {
        private readonly IHttpClientRequester<IRequestModel, IResponseModel> _innerRequestHandler;
        private readonly ILogger<HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>> _logger;

        public HttpClientRequesterLoggingDecorator(IHttpClientRequester<IRequestModel, IResponseModel> innerRequestHandler,
            ILogger<HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>> logger)
        {
            this._innerRequestHandler = innerRequestHandler;
            this._logger = logger;
        }

        public async Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var response =  await _innerRequestHandler.SendHttpClientRequest<TRequest, TResponse>(T, Url, HttpMethod);

            sw.Stop();
            long elapsedMillis = sw.ElapsedMilliseconds;

            _logger.LogInformation("Retrieved data {response} - Elapsed ms: {elapsedMillis}", JsonConvert.SerializeObject(response), elapsedMillis);

            return response;
        }

        public async Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod, params KeyValuePair<string, string>[] AdditionalHeaders)
        {
            return await _innerRequestHandler.SendHttpClientRequest<TRequest, TResponse>(T, Url, HttpMethod, AdditionalHeaders);
        }
    }
}
