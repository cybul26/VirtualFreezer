using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.AccountVerification.Application.Features.SendVerification;
using VirtualFreezer.AccountVerification.Domain.Repositories;
using VirtualFreezer.AccountVerification.Infrastructure.EF;
using VirtualFreezer.AccountVerification.Infrastructure.Repositories;
using VirtualFreezer.Shared.Infrastructure.DAL;
using VirtualFreezer.Shared.Infrastructure.Messaging;

namespace VirtualFreezer.AccountVerification.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<VerificationDbContext>(configuration);
        services.AddScoped<IVerificationsRepository, VerificationsRepository>();
        services.AddMessaging(configuration, typeof(UserRegisteredConsumer));
        return services;
    }
}