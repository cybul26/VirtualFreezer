using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Xunit;

namespace VirtualFreezer.Shared.Testing
{
    [Collection(nameof(TestApplicationFactory<TStartup>))]
    public abstract class WebApiTestBase<TStartup> : IDisposable where TStartup : class
    {

        private readonly Checkpoint _checkpoint = new()
        {
            TablesToIgnore = new Table[] {"__EFMigrationsHistory"},
            SchemasToInclude = new[]
            {
                "public"
            },
            DbAdapter = DbAdapter.Postgres
        };

        private static readonly JsonSerializerOptions? SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {new JsonStringEnumConverter()},
        };

        private readonly Type _dbContextType;
        private string? _route;
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<TStartup> _factory;

        protected WebApiTestBase(TestApplicationFactory<TStartup> factory, Type dbContextType)
        {
            _dbContextType = dbContextType;
            _factory = factory.WithWebHostBuilder(builder => { builder.ConfigureServices(ConfigureServices); });
            _client = _factory.CreateClient();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        protected T GetRequiredService<T>() where T : notnull
        {
            using var scope = _factory.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<T>();
            return service;
        }

        protected void SetPath(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                _route = string.Empty;
                return;
            }

            if (route.StartsWith("/"))
            {
                route = route.Substring(1, route.Length - 1);
            }

            if (route.EndsWith("/"))
            {
                route = route[..^1];
            }

            _route = $"{route}/";
        }

        protected static T? Map<T>(object data) =>
            JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(data, SerializerOptions), SerializerOptions);

        protected Task<HttpResponseMessage> GetAsync(string endpoint)
            => _client.GetAsync(GetEndpoint(endpoint));

        protected async Task<T?> GetAsync<T>(string endpoint)
            => await ReadAsync<T>(await GetAsync(endpoint));

        protected Task<HttpResponseMessage> PostAsync<T>(string endpoint, T command)
            => _client.PostAsync(GetEndpoint(endpoint), GetPayload(command!));

        protected Task<HttpResponseMessage> PostAsync(string endpoint)
            => _client.PostAsync(GetEndpoint(endpoint), null);

        protected Task<HttpResponseMessage> PutAsync<T>(string endpoint, T command)
            => _client.PutAsync(GetEndpoint(endpoint), GetPayload(command!));

        protected Task<HttpResponseMessage> DeleteAsync(string endpoint)
            => _client.DeleteAsync(GetEndpoint(endpoint));

        protected Task<HttpResponseMessage> SendAsync(string method, string endpoint)
            => SendAsync(GetMethod(method), endpoint);

        protected Task<HttpResponseMessage> SendAsync(HttpMethod method, string endpoint)
            => _client.SendAsync(new HttpRequestMessage(method, GetEndpoint(endpoint)));


        protected void Authenticate(Guid? userId = null)
        {
            var jwt = AuthHelper.GenerateJwt(userId ?? Guid.NewGuid());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }

        private static HttpMethod GetMethod(string method)
            => method.ToUpperInvariant() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException(nameof(method))
            };

        private string GetEndpoint(string endpoint) => $"{_route}{endpoint}";

        private static StringContent GetPayload(object value)
            => new(JsonSerializer.Serialize(value, SerializerOptions), Encoding.UTF8, "application/json");

        protected static async Task<T?> ReadAsync<T>(HttpResponseMessage response)
            => JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), SerializerOptions);

        protected async Task AddAsync<T>(T entity) where T : class
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService(_dbContextType) as DbContext ??
                          throw new ArgumentException($"Db context of type {_dbContextType.Name} was not found");
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService(_dbContextType) as DbContext ??
                          throw new ArgumentException($"Db context of type {_dbContextType.Name} was not found");
            var connectionString = context.Database.GetConnectionString() ??
                                   throw new ArgumentException(
                                       $"Connection string was not found for context: {_dbContextType.Name}");
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            _checkpoint.Reset(conn).GetAwaiter().GetResult();
        }
    }
}