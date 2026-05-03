namespace DecoratorPattern.API.Services.Decorators;

/// <summary>
/// Caching Decorator - Caches service operation results
/// This is a real-world use case of the Decorator Pattern
/// </summary>
public class CachingDecorator : ServiceDecoratorBase
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingDecorator> _logger;
    private const int CacheDurationMinutes = 5;

    /// <summary>
    /// Constructor that wraps the service and injects cache
    /// </summary>
    public CachingDecorator(IProductService innerService, IMemoryCache cache, ILogger<CachingDecorator> logger)
        : base(innerService)
    {
        _cache = cache;
        _logger = logger;
    }

    /// <summary>
    /// Gets from cache if available, otherwise calls wrapped service
    /// </summary>
    public override async Task<IEnumerable<Models.Product>> GetAllProductsAsync()
    {
        const string cacheKey = "all_products";

        if (_cache.TryGetValue(cacheKey, out IEnumerable<Models.Product>? cachedProducts))
        {
            _logger.LogInformation("[CACHE] Hit for key: {CacheKey}", cacheKey);
            return cachedProducts!;
        }

        _logger.LogInformation("[CACHE] Miss for key: {CacheKey}, calling service", cacheKey);
        var result = await base.GetAllProductsAsync();

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheDurationMinutes));

        _cache.Set(cacheKey, result, cacheOptions);
        return result;
    }

    /// <summary>
    /// Gets from cache if available, otherwise calls wrapped service
    /// </summary>
    public override async Task<Models.Product?> GetProductByIdAsync(int id)
    {
        string cacheKey = $"product_{id}";

        if (_cache.TryGetValue(cacheKey, out Models.Product? cachedProduct))
        {
            _logger.LogInformation("[CACHE] Hit for key: {CacheKey}", cacheKey);
            return cachedProduct;
        }

        _logger.LogInformation("[CACHE] Miss for key: {CacheKey}, calling service", cacheKey);
        var result = await base.GetProductByIdAsync(id);

        if (result != null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheDurationMinutes));

            _cache.Set(cacheKey, result, cacheOptions);
        }

        return result;
    }

    /// <summary>
    /// Creates product and invalidates relevant caches
    /// </summary>
    public override async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        var result = await base.CreateProductAsync(product);

        // Invalidate cache
        _cache.Remove("all_products");
        _logger.LogInformation("[CACHE] Invalidated cache for all_products");

        return result;
    }

    /// <summary>
    /// Updates product and invalidates relevant caches
    /// </summary>
    public override async Task<Models.Product> UpdateProductAsync(int id, Models.Product product)
    {
        var result = await base.UpdateProductAsync(id, product);

        // Invalidate caches
        _cache.Remove("all_products");
        _cache.Remove($"product_{id}");
        _logger.LogInformation("[CACHE] Invalidated caches for product {ProductId}", id);

        return result;
    }

    /// <summary>
    /// Deletes product and invalidates relevant caches
    /// </summary>
    public override async Task<bool> DeleteProductAsync(int id)
    {
        var result = await base.DeleteProductAsync(id);

        if (result)
        {
            // Invalidate caches
            _cache.Remove("all_products");
            _cache.Remove($"product_{id}");
            _logger.LogInformation("[CACHE] Invalidated caches for deleted product {ProductId}", id);
        }

        return result;
    }

    /// <summary>
    /// Searches products (no caching for search)
    /// </summary>
    public override async Task<IEnumerable<Models.Product>> SearchProductsAsync(string searchTerm)
    {
        return await base.SearchProductsAsync(searchTerm);
    }
}
