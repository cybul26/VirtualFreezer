using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualFreezer.AccountVerification.Application.Features.SendVerification;
using VirtualFreezer.AccountVerification.Application.Options;
using VirtualFreezer.Shared.Infrastructure;

namespace VirtualFreezer.AccountVerification.Application;

public static class Extensions
{
    private const string SectionName = "verification";

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.BindOptions<VerificationOptions>(SectionName);
        if (string.IsNullOrEmpty(options.VerificationUrl))
        {
            throw new ArgumentException("Verififaction url is required in appsettings.json");
        }

        services.AddSingleton(options);
        services.AddSingleton<IVerificationMailFactory, VerificationMailFactory>();
        return services;
    }
}