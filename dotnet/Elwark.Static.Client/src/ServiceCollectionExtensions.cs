using System;
using Elwark.Static.Client.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Static.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddElwarkStaticClient(this IServiceCollection services, Uri host)
        {
            return services.AddHttpClient<IElwarkStaticClient, ElwarkStaticClient>(client => client.BaseAddress = host);
        }
    }
}