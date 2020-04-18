using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Elwark.Storage.Client.Test
{
    public abstract class BaseTest : IDisposable
    {
        private readonly Uri _host = new Uri("http://localhost:3000");
        protected TestServer Server { get; }

        public BaseTest()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(collection => collection.AddElwarkStorageClient(_host))
                .Configure(applicationBuilder => { });

            Server = new TestServer(builder);
        }

        public void Dispose() =>
            Server.Dispose();
    }
}