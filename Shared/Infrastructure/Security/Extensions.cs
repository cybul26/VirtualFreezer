using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.Shared.Infrastructure.Security.Random;

namespace VirtualFreezer.Shared.Infrastructure.Security;

public static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IRng, Rng>()
            .AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

        return services;
    }
}