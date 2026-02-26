using System.Reflection;
using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Extensions;

public static class CarterExtensions
{
    public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddCarter(configurator: cfg =>
        {
            foreach (var assembly in assemblies)
            {
                var mods = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
                    .ToArray();
                cfg.WithModules(mods);
            }
        });

        return services;
    }
}