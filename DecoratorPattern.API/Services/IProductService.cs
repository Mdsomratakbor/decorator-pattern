namespace DecoratorPattern.API.Services;

/// <summary>
/// Interface for product service - defines the contract
/// that real service and decorators must implement
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Gets all products
    /// </summary>
    Task<IEnumerable<Models.Product>> GetAllProductsAsync();

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    Task<Models.Product?> GetProductByIdAsync(int id);

    /// <summary>
    /// Creates a new product
    /// </summary>
    Task<Models.Product> CreateProductAsync(Models.Product product);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    Task<Models.Product> UpdateProductAsync(int id, Models.Product product);

    /// <summary>
    /// Deletes a product
    /// </summary>
    Task<bool> DeleteProductAsync(int id);

    /// <summary>
    /// Searches products by name
    /// </summary>
    Task<IEnumerable<Models.Product>> SearchProductsAsync(string searchTerm);
}
