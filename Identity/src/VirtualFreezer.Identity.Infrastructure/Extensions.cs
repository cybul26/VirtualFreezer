using System.Runtime.CompilerServices;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using VirtualFreezer.Identity.Application.Features.SignUp;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.Identity.Infrastructure.DataInitializers;
using VirtualFreezer.Identity.Infrastructure.EF;
using VirtualFreezer.Identity.Infrastructure.Repositories;
using VirtualFreezer.Shared.Infrastructure.DAL;
using VirtualFreezer.Shared.Infrastructure.Messaging;

[assembly: InternalsVisibleTo("VirtualFreezer.Identity.Tests.Integration")]

namespace VirtualFreezer.Identity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<IdentityDbContext>(configuration)
            .AddScoped<IUserRepository, UserRepository>()
            .AddInitializer<DatabaseDataInitializer>()
            .AddMessaging(configuration, typeof(AccountVerifiedConsumer));

        return services;
    }
}