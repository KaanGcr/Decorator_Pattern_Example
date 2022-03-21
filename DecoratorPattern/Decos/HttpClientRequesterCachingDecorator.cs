using DecoratorPattern.Models;
using DecoratorPattern.Services;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorPattern.Decos
{
    public class HttpClientRequesterCachingDecorator<T, K> : IHttpClientRequester<T, K>
        where T : IRequestModel
        where K : IResponseModel
    {
        private readonly IHttpClientRequester<IRequestModel, IResponseModel> _innerRequestHandler;
        private readonly IMemoryCache _cache;

        public HttpClientRequesterCachingDecorator(IHttpClientRequester<IRequestModel, IResponseModel> innerRequestHandler,
            IMemoryCache cache)
        {
            this._innerRequestHandler = innerRequestHandler;
            this._cache = cache;
        }

        public async Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod)
        {
            string cacheKey = "Currencies";

            if (_cache.TryGetValue<CurrencyModel>(cacheKey, out var currencies
                ))
            {
                return currencies;
            }

            var response = await _innerRequestHandler.SendHttpClientRequest<TRequest, TResponse>(T, Url, HttpMethod);

            return response;
        }

        public Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod, params KeyValuePair<string, string>[] AdditionalHeaders)
        {
            throw new NotImplementedException();
        }
    }
}
