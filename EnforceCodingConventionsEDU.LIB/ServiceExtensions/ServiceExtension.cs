using EnforceCodingConventionsEDU.LIB.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace EnforceCodingConventionsEDU.LIB.ServiceExtensions;

/// <summary>
/// Class for extending the dependency injection registration of the caller with the library registrations.
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    /// Registers the class library interfaces and implementations as extension method for the calling application.
    /// </summary>
    public static IServiceCollection AddLibServices(this IServiceCollection services)
    {
        services.AddSingleton<SomeUtilityClassInterface, SomeUtilityClass>();

        return services;
    }
}