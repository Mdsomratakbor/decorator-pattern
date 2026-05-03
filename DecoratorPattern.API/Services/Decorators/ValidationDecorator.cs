namespace DecoratorPattern.API.Services.Decorators;

/// <summary>
/// Validation Decorator - Validates all operations
/// This is a real-world use case of the Decorator Pattern
/// </summary>
public class ValidationDecorator : ServiceDecoratorBase
{
    private readonly ILogger<ValidationDecorator> _logger;

    /// <summary>
    /// Constructor that wraps the service
    /// </summary>
    public ValidationDecorator(IProductService innerService, ILogger<ValidationDecorator> logger)
        : base(innerService)
    {
        _logger = logger;
    }

    /// <summary>
    /// Validates before creating product
    /// </summary>
    public override async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        _logger.LogInformation("[VALIDATION] Validating new product: {ProductName}", product.Name);

        // Validate
        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null");

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required", nameof(product.Name));

        if (product.Name.Length < 3)
            throw new ArgumentException("Product name must be at least 3 characters long", nameof(product.Name));

        if (product.Price <= 0)
            throw new ArgumentException("Product price must be greater than zero", nameof(product.Price));

        if (product.Price > 1000000)
            throw new ArgumentException("Product price cannot exceed 1,000,000", nameof(product.Price));

        if (product.Quantity < 0)
            throw new ArgumentException("Product quantity cannot be negative", nameof(product.Quantity));

        _logger.LogInformation("[VALIDATION] Validation passed for product: {ProductName}", product.Name);
        return await base.CreateProductAsync(product);
    }

    /// <summary>
    /// Validates before updating product
    /// </summary>
    public override async Task<Models.Product> UpdateProductAsync(int id, Models.Product product)
    {
        _logger.LogInformation("[VALIDATION] Validating product update for ID: {ProductId}", id);

        if (id <= 0)
            throw new ArgumentException("Product ID must be greater than zero", nameof(id));

        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null");

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required", nameof(product.Name));

        if (product.Price <= 0)
            throw new ArgumentException("Product price must be greater than zero", nameof(product.Price));

        if (product.Quantity < 0)
            throw new ArgumentException("Product quantity cannot be negative", nameof(product.Quantity));

        _logger.LogInformation("[VALIDATION] Validation passed for product update ID: {ProductId}", id);
        return await base.UpdateProductAsync(id, product);
    }

    /// <summary>
    /// Validates before deleting product
    /// </summary>
    public override async Task<bool> DeleteProductAsync(int id)
    {
        _logger.LogInformation("[VALIDATION] Validating product deletion for ID: {ProductId}", id);

        if (id <= 0)
            throw new ArgumentException("Product ID must be greater than zero", nameof(id));

        _logger.LogInformation("[VALIDATION] Validation passed for product deletion ID: {ProductId}", id);
        return await base.DeleteProductAsync(id);
    }
}
