using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Storage.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddElwarkStorageClient(this IServiceCollection services, Uri host) =>
            services.AddHttpClient<IElwarkStorageClient, ElwarkStorageClient>(client => client.BaseAddress = host);
    }
}