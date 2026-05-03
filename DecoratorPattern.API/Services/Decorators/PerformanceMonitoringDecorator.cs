namespace DecoratorPattern.API.Services.Decorators;

/// <summary>
/// Performance Monitoring Decorator - Monitors execution time and metrics
/// This is a real-world use case of the Decorator Pattern
/// </summary>
public class PerformanceMonitoringDecorator : ServiceDecoratorBase
{
    private readonly ILogger<PerformanceMonitoringDecorator> _logger;

    /// <summary>
    /// Constructor that wraps the service
    /// </summary>
    public PerformanceMonitoringDecorator(IProductService innerService, ILogger<PerformanceMonitoringDecorator> logger)
        : base(innerService)
    {
        _logger = logger;
    }

    /// <summary>
    /// Monitors performance of GetAllProducts
    /// </summary>
    public override async Task<IEnumerable<Models.Product>> GetAllProductsAsync()
    {
        var startTime = DateTime.UtcNow;
        var startMemory = GC.GetTotalMemory(false);

        _logger.LogInformation("[PERF] Starting GetAllProducts");

        var result = await base.GetAllProductsAsync();

        var endTime = DateTime.UtcNow;
        var endMemory = GC.GetTotalMemory(false);
        var executionTime = (endTime - startTime).TotalMilliseconds;
        var memoryUsed = endMemory - startMemory;

        _logger.LogInformation("[PERF] GetAllProducts completed in {ExecutionTimeMs}ms, Memory: {MemoryUsed} bytes", executionTime, memoryUsed);

        if (executionTime > 1000)
        {
            _logger.LogWarning("[PERF] GetAllProducts is slow! Took {ExecutionTimeMs}ms", executionTime);
        }

        return result;
    }

    /// <summary>
    /// Monitors performance of GetProductById
    /// </summary>
    public override async Task<Models.Product?> GetProductByIdAsync(int id)
    {
        var startTime = DateTime.UtcNow;
        _logger.LogInformation("[PERF] Starting GetProductById for ID: {ProductId}", id);

        var result = await base.GetProductByIdAsync(id);

        var endTime = DateTime.UtcNow;
        var executionTime = (endTime - startTime).TotalMilliseconds;
        _logger.LogInformation("[PERF] GetProductById completed in {ExecutionTimeMs}ms", executionTime);

        return result;
    }

    /// <summary>
    /// Monitors performance of CreateProduct
    /// </summary>
    public override async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        var startTime = DateTime.UtcNow;
        _logger.LogInformation("[PERF] Starting CreateProduct");

        var result = await base.CreateProductAsync(product);

        var endTime = DateTime.UtcNow;
        var executionTime = (endTime - startTime).TotalMilliseconds;
        _logger.LogInformation("[PERF] CreateProduct completed in {ExecutionTimeMs}ms", executionTime);

        return result;
    }

    /// <summary>
    /// Monitors performance of UpdateProduct
    /// </summary>
    public override async Task<Models.Product> UpdateProductAsync(int id, Models.Product product)
    {
        var startTime = DateTime.UtcNow;
        _logger.LogInformation("[PERF] Starting UpdateProduct for ID: {ProductId}", id);

        var result = await base.UpdateProductAsync(id, product);

        var endTime = DateTime.UtcNow;
        var executionTime = (endTime - startTime).TotalMilliseconds;
        _logger.LogInformation("[PERF] UpdateProduct completed in {ExecutionTimeMs}ms", executionTime);

        return result;
    }

    /// <summary>
    /// Monitors performance of DeleteProduct
    /// </summary>
    public override async Task<bool> DeleteProductAsync(int id)
    {
        var startTime = DateTime.UtcNow;
        _logger.LogInformation("[PERF] Starting DeleteProduct for ID: {ProductId}", id);

        var result = await base.DeleteProductAsync(id);

        var endTime = DateTime.UtcNow;
        var executionTime = (endTime - startTime).TotalMilliseconds;
        _logger.LogInformation("[PERF] DeleteProduct completed in {ExecutionTimeMs}ms", executionTime);

        return result;
    }

    /// <summary>
    /// Monitors performance of SearchProducts
    /// </summary>
    public override async Task<IEnumerable<Models.Product>> SearchProductsAsync(string searchTerm)
    {
        var startTime = DateTime.UtcNow;
        _logger.LogInformation("[PERF] Starting SearchProducts for: {SearchTerm}", searchTerm);

        var result = await base.SearchProductsAsync(searchTerm);

        var endTime = DateTime.UtcNow;
        var executionTime = (endTime - startTime).TotalMilliseconds;
        _logger.LogInformation("[PERF] SearchProducts completed in {ExecutionTimeMs}ms", executionTime);

        return result;
    }
}
