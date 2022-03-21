using Autofac;
using DecoratorPattern.Decos;
using DecoratorPattern.Models;
using DecoratorPattern.Services;

namespace DecoratorPattern.Utilities
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClientRequester<IRequestModel, IResponseModel>>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterDecorator<HttpClientRequesterLoggingDecorator<IRequestModel, IResponseModel>, IHttpClientRequester<IRequestModel, IResponseModel>>();

            builder.RegisterDecorator<HttpClientRequesterCachingDecorator<IRequestModel, IResponseModel>, IHttpClientRequester<IRequestModel, IResponseModel>>();
        }
    }
}
