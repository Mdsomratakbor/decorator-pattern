namespace DecoratorPattern.API.Services.Decorators;

/// <summary>
/// Logging Decorator - Logs all service operations
/// This is a real-world use case of the Decorator Pattern
/// </summary>
public class LoggingDecorator : ServiceDecoratorBase
{
    private readonly ILogger<LoggingDecorator> _logger;

    /// <summary>
    /// Constructor that wraps the service and injects logger
    /// </summary>
    public LoggingDecorator(IProductService innerService, ILogger<LoggingDecorator> logger)
        : base(innerService)
    {
        _logger = logger;
    }

    /// <summary>
    /// Logs the operation and delegates to wrapped service
    /// </summary>
    public override async Task<IEnumerable<Models.Product>> GetAllProductsAsync()
    {
        _logger.LogInformation("[LOG] Getting all products");
        var result = await base.GetAllProductsAsync();
        _logger.LogInformation("[LOG] Retrieved {Count} products", result.Count());
        return result;
    }

    /// <summary>
    /// Logs the operation and delegates to wrapped service
    /// </summary>
    public override async Task<Models.Product?> GetProductByIdAsync(int id)
    {
        _logger.LogInformation("[LOG] Getting product with ID: {ProductId}", id);
        var result = await base.GetProductByIdAsync(id);
        _logger.LogInformation("[LOG] Product found: {ProductName}", result?.Name ?? "Not found");
        return result;
    }

    /// <summary>
    /// Logs the operation and delegates to wrapped service
    /// </summary>
    public override async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        _logger.LogInformation("[LOG] Creating product: {ProductName}", product.Name);
        var result = await base.CreateProductAsync(product);
        _logger.LogInformation("[LOG] Product created with ID: {ProductId}", result.Id);
        return result;
    }

    /// <summary>
    /// Logs the operation and delegates to wrapped service
    /// </summary>
    public override async Task<Models.Product> UpdateProductAsync(int id, Models.Product product)
    {
        _logger.LogInformation("[LOG] Updating product ID: {ProductId}", id);
        var result = await base.UpdateProductAsync(id, product);
        _logger.LogInformation("[LOG] Product updated successfully");
        return result;
    }

    /// <summary>
    /// Logs the operation and delegates to wrapped service
    /// </summary>
    public override async Task<bool> DeleteProductAsync(int id)
    {
        _logger.LogInformation("[LOG] Deleting product ID: {ProductId}", id);
        var result = await base.DeleteProductAsync(id);
        _logger.LogInformation("[LOG] Product deleted: {Success}", result);
        return result;
    }

    /// <summary>
    /// Logs the operation and delegates to wrapped service
    /// </summary>
    public override async Task<IEnumerable<Models.Product>> SearchProductsAsync(string searchTerm)
    {
        _logger.LogInformation("[LOG] Searching products for: {SearchTerm}", searchTerm);
        var result = await base.SearchProductsAsync(searchTerm);
        _logger.LogInformation("[LOG] Found {Count} products matching '{SearchTerm}'", result.Count(), searchTerm);
        return result;
    }
}
