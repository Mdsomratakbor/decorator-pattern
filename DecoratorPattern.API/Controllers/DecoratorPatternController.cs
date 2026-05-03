using DecoratorPattern.API.Models;
using DecoratorPattern.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPattern.API.Controllers;

/// <summary>
/// Controller that demonstrates the decorator pattern setup and configuration
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DecoratorPatternController : ControllerBase
{
    /// <summary>
    /// Gets information about how the decorator pattern is implemented in this API
    /// </summary>
    [HttpGet("info")]
    public ActionResult<object> GetDecoratorPatternInfo()
    {
        var info = new
        {
            title = "Decorator Pattern Implementation in ASP.NET Core API",
            description = "This API demonstrates real-world use of the Decorator Pattern for cross-cutting concerns",
            decoratorsApplied = new string[]
            {
                "LoggingDecorator - Logs all service operations",
                "CachingDecorator - Caches GET operations and invalidates on modifications",
                "ValidationDecorator - Validates input before operations",
                "PerformanceMonitoringDecorator - Monitors execution time and memory usage"
            },
            serviceDecoratorStack = new string[]
            {
                "ProductService (Core)",
                "ValidationDecorator (validates input)",
                "PerformanceMonitoringDecorator (monitors performance)",
                "CachingDecorator (caches results)",
                "LoggingDecorator (logs operations)"
            },
            benefits = new string[]
            {
                "Separation of Concerns - Each decorator has one responsibility",
                "Open/Closed Principle - Open for extension, closed for modification",
                "Reusability - Decorators can be applied to any implementation of IProductService",
                "Composability - Stack decorators in any order",
                "Testability - Each decorator can be tested independently"
            },
            examples = new
            {
                getAllProducts = new { method = "GET", endpoint = "/api/products", description = "Gets all products with caching" },
                getProductById = new { method = "GET", endpoint = "/api/products/{id}", description = "Gets a product with caching" },
                createProduct = new { method = "POST", endpoint = "/api/products", description = "Creates a product with validation" },
                updateProduct = new { method = "PUT", endpoint = "/api/products/{id}", description = "Updates a product with validation" },
                deleteProduct = new { method = "DELETE", endpoint = "/api/products/{id}", description = "Deletes a product with validation" },
                searchProducts = new { method = "GET", endpoint = "/api/products/search/{term}", description = "Searches products" }
            }
        };

        return Ok(info);
    }

    /// <summary>
    /// Gets explanation of each decorator
    /// </summary>
    [HttpGet("decorators")]
    public ActionResult<object> GetDecoratorsExplanation()
    {
        var decorators = new
        {
            logginDecorator = new
            {
                name = "LoggingDecorator",
                purpose = "Logs all service method calls before and after execution",
                realWorldUseCases = new[] { "Audit trails", "Activity monitoring", "Debugging" },
                example = "When you call GetProductById(1), it logs: '[LOG] Getting product with ID: 1' and '[LOG] Product found: Laptop'"
            },
            cachingDecorator = new
            {
                name = "CachingDecorator",
                purpose = "Caches GET operation results to improve performance",
                realWorldUseCases = new[] { "Performance improvement", "Reducing database load", "Offline functionality" },
                example = "First call to GetAllProducts() hits the database, subsequent calls return from cache for 5 minutes"
            },
            validationDecorator = new
            {
                name = "ValidationDecorator",
                purpose = "Validates input before passing to the service",
                realWorldUseCases = new[] { "Input validation", "Business rule enforcement", "Data consistency" },
                example = "CreateProduct() validates that name is not empty, price is positive, etc. before creating"
            },
            performanceMonitoringDecorator = new
            {
                name = "PerformanceMonitoringDecorator",
                purpose = "Monitors execution time and alerts on slow operations",
                realWorldUseCases = new[] { "Performance monitoring", "SLA tracking", "Bottleneck identification" },
                example = "GetAllProducts() logs how many milliseconds it took and warns if > 1 second"
            }
        };

        return Ok(decorators);
    }
}
