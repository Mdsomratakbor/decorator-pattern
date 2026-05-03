namespace DecoratorPattern.API.Services;

/// <summary>
/// Extension methods for adding decorators to services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Decorates an existing service with a decorator function
    /// </summary>
    /// <typeparam name="TInterface">The interface being decorated</typeparam>
    /// <param name="services">The service collection</param>
    /// <param name="decorator">The decorator function</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection Decorate<TInterface>(
        this IServiceCollection services,
        Func<TInterface, IServiceProvider, TInterface> decorator)
        where TInterface : class
    {
        // Get the current registration
        var wrappedDescriptor = services.FirstOrDefault(x => x.ServiceType == typeof(TInterface));
        
        if (wrappedDescriptor == null)
        {
            throw new InvalidOperationException($"{typeof(TInterface).Name} is not registered");
        }

        // Create a factory that creates decorated instances
        var objectFactory = ActivatorUtilities.CreateFactory(wrappedDescriptor.ImplementationType!, Array.Empty<Type>());
        
        services.Replace(ServiceDescriptor.Describe(
            typeof(TInterface),
            provider => decorator((TInterface)objectFactory(provider, Array.Empty<object?>()), provider),
            wrappedDescriptor.Lifetime
        ));

        return services;
    }
}
