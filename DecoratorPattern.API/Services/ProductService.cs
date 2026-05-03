namespace DecoratorPattern.API.Services;

/// <summary>
/// Concrete implementation of the product service.
/// This is the base service that will be decorated.
/// </summary>
public class ProductService : IProductService
{
    // In-memory storage for demo purposes
    private static readonly List<Models.Product> _products = new()
    {
        new Models.Product
        {
            Id = 1,
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 999.99m,
            Quantity = 10,
            CreatedAt = DateTime.UtcNow.AddDays(-30),
            UpdatedAt = DateTime.UtcNow.AddDays(-5)
        },
        new Models.Product
        {
            Id = 2,
            Name = "Mouse",
            Description = "Wireless mouse",
            Price = 29.99m,
            Quantity = 100,
            CreatedAt = DateTime.UtcNow.AddDays(-60),
            UpdatedAt = DateTime.UtcNow.AddDays(-2)
        },
        new Models.Product
        {
            Id = 3,
            Name = "Keyboard",
            Description = "Mechanical keyboard",
            Price = 149.99m,
            Quantity = 50,
            CreatedAt = DateTime.UtcNow.AddDays(-45),
            UpdatedAt = DateTime.UtcNow
        }
    };

    /// <summary>
    /// Gets all products
    /// </summary>
    public async Task<IEnumerable<Models.Product>> GetAllProductsAsync()
    {
        // Simulate database call
        await Task.Delay(100);
        return _products.AsReadOnly();
    }

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    public async Task<Models.Product?> GetProductByIdAsync(int id)
    {
        // Simulate database call
        await Task.Delay(100);
        return _products.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    public async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        // Validate
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required");
        if (product.Price <= 0)
            throw new ArgumentException("Product price must be greater than zero");

        // Simulate database call
        await Task.Delay(100);

        product.Id = _products.Max(p => p.Id) + 1;
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _products.Add(product);
        return product;
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    public async Task<Models.Product> UpdateProductAsync(int id, Models.Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing == null)
            throw new KeyNotFoundException($"Product with ID {id} not found");

        // Simulate database call
        await Task.Delay(100);

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Quantity = product.Quantity;
        existing.UpdatedAt = DateTime.UtcNow;

        return existing;
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return false;

        // Simulate database call
        await Task.Delay(100);

        _products.Remove(product);
        return true;
    }

    /// <summary>
    /// Searches products by name
    /// </summary>
    public async Task<IEnumerable<Models.Product>> SearchProductsAsync(string searchTerm)
    {
        // Simulate database call
        await Task.Delay(100);

        return _products
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
