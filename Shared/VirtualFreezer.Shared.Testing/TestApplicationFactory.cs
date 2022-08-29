using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace VirtualFreezer.Shared.Testing
{
    public class TestApplicationFactory<T> : WebApplicationFactory<T>, IAsyncLifetime where T : class
    {
        private readonly TestcontainersContainer _dockerContainer;

        public TestApplicationFactory()
        {
            _dockerContainer = DockerTestContainerHelper.CreateSqlDockerImage();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("test");
        }

        public Task InitializeAsync()
            => _dockerContainer.StartAsync();

        public new Task DisposeAsync()
            => _dockerContainer.StopAsync();
    }
}