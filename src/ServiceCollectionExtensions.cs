using System;
using Elwark.Storage.Client.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Storage.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddElwarkStorageClient(this IServiceCollection services, Uri host)
        {
            return services.AddHttpClient<IElwarkStorageClient, ElwarkStorageClient>(client => client.BaseAddress = host);
        }
    }
}