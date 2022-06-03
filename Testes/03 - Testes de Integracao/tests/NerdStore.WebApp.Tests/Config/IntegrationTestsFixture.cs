using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace NerdStore.WebApp.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>>
    {

    }

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>>
    {

    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly LojaAppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {

            };

            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }
        public void Dispose()
        {
            Client?.Dispose();
            Factory?.Dispose();
        }
    }
}