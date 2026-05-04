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
        // Match the container's default behavior: a single service resolve uses the last registration.
        var wrappedDescriptor = services.LastOrDefault(x => x.ServiceType == typeof(TInterface));
        
        if (wrappedDescriptor == null)
        {
            throw new InvalidOperationException($"{typeof(TInterface).Name} is not registered");
        }
        
        // Remove the existing registration
        services.Remove(wrappedDescriptor);
        
        // Add the new decorated registration
        services.Add(ServiceDescriptor.Describe(
            typeof(TInterface),
            provider => decorator(CreateInstance<TInterface>(provider, wrappedDescriptor), provider),
            wrappedDescriptor.Lifetime
        ));

        return services;
    }

    private static TInterface CreateInstance<TInterface>(IServiceProvider provider, ServiceDescriptor descriptor)
        where TInterface : class
    {
        if (descriptor.ImplementationInstance is TInterface implementationInstance)
        {
            return implementationInstance;
        }

        if (descriptor.ImplementationFactory is not null)
        {
            return (TInterface)descriptor.ImplementationFactory(provider);
        }

        if (descriptor.ImplementationType is not null)
        {
            return (TInterface)ActivatorUtilities.CreateInstance(provider, descriptor.ImplementationType);
        }

        throw new InvalidOperationException(
            $"Unable to decorate {typeof(TInterface).Name} because the existing registration has no implementation.");
    }
}
