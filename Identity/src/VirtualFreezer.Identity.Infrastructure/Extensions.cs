using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.Identity.Infrastructure.DataInitializers;
using VirtualFreezer.Identity.Infrastructure.EF;
using VirtualFreezer.Identity.Infrastructure.Repositories;
using VirtualFreezer.Shared.Infrastructure.DAL;

namespace VirtualFreezer.Identity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<IdentityDbContext>(configuration)
            .AddScoped<IUserRepository, UserRepository>()
            .AddInitializer<DatabaseDataInitializer>();
        return services;
    }
}