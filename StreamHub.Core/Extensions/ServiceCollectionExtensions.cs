using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Core.Extensions;

/// <summary>
///     Extension methods for dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Registers all repository implementations as scoped services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="assembly">The assembly to scan for repositories.</param>
    public static void AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var repositoryTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IGenericRepository<>)));

        foreach (var type in repositoryTypes)
        {
            var interfaceType = type.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IGenericRepository<>));
            services.AddScoped(interfaceType, type);
        }
    }
}