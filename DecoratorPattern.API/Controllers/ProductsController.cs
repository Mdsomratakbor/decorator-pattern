using DecoratorPattern.API.Models;
using DecoratorPattern.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPattern.API.Controllers;

/// <summary>
/// Controller for Product operations demonstrating decorator pattern
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    /// <summary>
    /// Constructor that injects the decorated service
    /// </summary>
    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all products
    /// Decorators applied: Logging, Caching, Performance Monitoring
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Product>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<Product>>>> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(ApiResponse<IEnumerable<Product>>.SuccessResponse(
                products,
                $"Retrieved {products.Count()} products successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all products");
            return StatusCode(500, ApiResponse<IEnumerable<Product>>.ErrorResponse(
                ex.Message,
                "Failed to retrieve products"));
        }
    }

    /// <summary>
    /// Gets a product by ID
    /// Decorators applied: Logging, Caching, Performance Monitoring
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<Product>>> GetProductById(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(ApiResponse<Product>.ErrorResponse(
                    $"Product with ID {id} not found",
                    "Product not found"));
            }

            return Ok(ApiResponse<Product>.SuccessResponse(product, "Product retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product by ID");
            return StatusCode(500, ApiResponse<Product>.ErrorResponse(
                ex.Message,
                "Failed to retrieve product"));
        }
    }

    /// <summary>
    /// Creates a new product
    /// Decorators applied: Validation, Logging, Caching (invalidation), Performance Monitoring
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<Product>>> CreateProduct(Product product)
    {
        try
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id },
                ApiResponse<Product>.SuccessResponse(createdProduct, "Product created successfully"));
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error creating product");
            return BadRequest(ApiResponse<Product>.ErrorResponse(
                ex.Message,
                "Validation failed"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return StatusCode(500, ApiResponse<Product>.ErrorResponse(
                ex.Message,
                "Failed to create product"));
        }
    }

    /// <summary>
    /// Updates a product
    /// Decorators applied: Validation, Logging, Caching (invalidation), Performance Monitoring
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<Product>>> UpdateProduct(int id, Product product)
    {
        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            return Ok(ApiResponse<Product>.SuccessResponse(updatedProduct, "Product updated successfully"));
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error updating product");
            return BadRequest(ApiResponse<Product>.ErrorResponse(
                ex.Message,
                "Validation failed"));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Product not found for update");
            return NotFound(ApiResponse<Product>.ErrorResponse(
                ex.Message,
                "Product not found"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product");
            return StatusCode(500, ApiResponse<Product>.ErrorResponse(
                ex.Message,
                "Failed to update product"));
        }
    }

    /// <summary>
    /// Deletes a product
    /// Decorators applied: Validation, Logging, Caching (invalidation), Performance Monitoring
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
    {
        try
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.ErrorResponse(
                    $"Product with ID {id} not found",
                    "Product not found"));
            }

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error deleting product");
            return BadRequest(ApiResponse<bool>.ErrorResponse(
                ex.Message,
                "Validation failed"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product");
            return StatusCode(500, ApiResponse<bool>.ErrorResponse(
                ex.Message,
                "Failed to delete product"));
        }
    }

    /// <summary>
    /// Searches for products by name
    /// Decorators applied: Logging, Performance Monitoring
    /// </summary>
    [HttpGet("search/{searchTerm}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Product>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<Product>>>> SearchProducts(string searchTerm)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(ApiResponse<IEnumerable<Product>>.ErrorResponse(
                    "Search term cannot be empty",
                    "Invalid search term"));
            }

            var products = await _productService.SearchProductsAsync(searchTerm);
            return Ok(ApiResponse<IEnumerable<Product>>.SuccessResponse(
                products,
                $"Found {products.Count()} products matching '{searchTerm}'"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products");
            return StatusCode(500, ApiResponse<IEnumerable<Product>>.ErrorResponse(
                ex.Message,
                "Search failed"));
        }
    }
}
