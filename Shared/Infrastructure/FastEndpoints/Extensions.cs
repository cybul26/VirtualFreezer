using System.Reflection;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace VirtualFreezer.Shared.Infrastructure.FastEndpoints;

internal static class Extensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints(x => x.Assemblies = GetSolutionAssemblies());
        return services;
    }

    private static IEnumerable<Assembly> GetSolutionAssemblies()
    {
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        return assemblies.ToArray();
    }

    public static IApplicationBuilder UseEndpoints(this WebApplication builder)
    {
        builder.UseFastEndpoints(c =>
        {
            c.ErrorResponseBuilder = (failures, _) => throw new ValidationException(failures);
        });
        return builder;
    }
}