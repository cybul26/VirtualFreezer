using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace VirtualFreezer.Shared.Testing;

internal static class DockerTestContainerHelper
{
    public static TestcontainersContainer CreateSqlDockerImage()
    {
        var connectionString = OptionsHelper.GetValue<string>("postgres:connectionString");

        var connectionBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var port = connectionBuilder.Port;
        return new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("postgres")
            .WithName($"freezer-integration-tests-postgres-{Guid.NewGuid():N}")
            .WithEnvironment("POSTGRES_HOST_AUTH_METHOD", "trust")
            .WithPortBinding(port, 5432)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();
    }
}