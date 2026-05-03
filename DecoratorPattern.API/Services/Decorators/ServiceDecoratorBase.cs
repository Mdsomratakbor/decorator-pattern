namespace DecoratorPattern.API.Services.Decorators;

/// <summary>
/// Base abstract class for service decorators.
/// All decorators inherit from this and wrap the IProductService.
/// </summary>
public abstract class ServiceDecoratorBase : IProductService
{
    /// <summary>
    /// The wrapped service
    /// </summary>
    protected readonly IProductService _innerService;

    /// <summary>
    /// Constructor that wraps the service
    /// </summary>
    /// <param name="innerService">The service to be decorated</param>
    protected ServiceDecoratorBase(IProductService innerService)
    {
        _innerService = innerService;
    }

    /// <summary>
    /// Default implementation delegates to wrapped service
    /// </summary>
    public virtual async Task<IEnumerable<Models.Product>> GetAllProductsAsync()
    {
        return await _innerService.GetAllProductsAsync();
    }

    /// <summary>
    /// Default implementation delegates to wrapped service
    /// </summary>
    public virtual async Task<Models.Product?> GetProductByIdAsync(int id)
    {
        return await _innerService.GetProductByIdAsync(id);
    }

    /// <summary>
    /// Default implementation delegates to wrapped service
    /// </summary>
    public virtual async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        return await _innerService.CreateProductAsync(product);
    }

    /// <summary>
    /// Default implementation delegates to wrapped service
    /// </summary>
    public virtual async Task<Models.Product> UpdateProductAsync(int id, Models.Product product)
    {
        return await _innerService.UpdateProductAsync(id, product);
    }

    /// <summary>
    /// Default implementation delegates to wrapped service
    /// </summary>
    public virtual async Task<bool> DeleteProductAsync(int id)
    {
        return await _innerService.DeleteProductAsync(id);
    }

    /// <summary>
    /// Default implementation delegates to wrapped service
    /// </summary>
    public virtual async Task<IEnumerable<Models.Product>> SearchProductsAsync(string searchTerm)
    {
        return await _innerService.SearchProductsAsync(searchTerm);
    }
}
