using DecoratorPattern.Models;

namespace DecoratorPattern.Services
{
    public interface IHttpClientRequester<T, K>
        where T : IRequestModel
        where K : IResponseModel
    {
        Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod);
        Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod, params KeyValuePair<string, string>[] AdditionalHeaders);
    }
}
