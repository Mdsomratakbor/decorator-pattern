# Decorator Pattern - API Project Documentation

## Overview

This ASP.NET Core API project demonstrates real-world application of the **Decorator Pattern** for handling cross-cutting concerns in a product management API.

## Architecture

### Service Decoration Stack

The API uses a layered decorator approach to wrap the core `ProductService`:

```
Request
  ↓
┌─────────────────────────────────────┐
│   LoggingDecorator                  │ ← First to process
│   (Logs all operations)             │
└──────────┬──────────────────────────┘
           ↓
┌─────────────────────────────────────┐
│   CachingDecorator                  │
│   (Caches GET results)              │
└──────────┬──────────────────────────┘
           ↓
┌─────────────────────────────────────┐
│   PerformanceMonitoringDecorator    │
│   (Measures execution time)         │
└──────────┬──────────────────────────┘
           ↓
┌─────────────────────────────────────┐
│   ValidationDecorator               │
│   (Validates input)                 │
└──────────┬──────────────────────────┘
           ↓
┌─────────────────────────────────────┐
│   ProductService (Core)             │
│   (Actual business logic)           │
└─────────────────────────────────────┘
```

## Decorators Explained

### 1. LoggingDecorator

**Purpose**: Logs all service operations for audit and debugging

**Features**:
- Logs method entry with parameters
- Logs method exit with results
- Logs operation counts and details

**Example Log Output**:
```
[LOG] Getting all products
[LOG] Retrieved 3 products
```

**Real-World Use Cases**:
- Activity auditing
- Debugging production issues
- Performance analysis
- Security monitoring

### 2. CachingDecorator

**Purpose**: Improves performance by caching read operations

**Features**:
- Caches GET results for 5 minutes
- Invalidates cache on CREATE, UPDATE, DELETE
- Logs cache hits and misses

**Example**:
- First request to `/api/products` → Hits database, stores in cache (100ms)
- Second request within 5 minutes → Returns from cache (< 5ms)
- Third request after 5 minutes → Hits database again (100ms)

**Real-World Use Cases**:
- Performance optimization
- Reducing database load
- Improving user experience
- Enabling offline functionality

### 3. PerformanceMonitoringDecorator

**Purpose**: Monitors operation execution time and detects slow operations

**Features**:
- Measures execution time in milliseconds
- Tracks memory usage
- Logs warnings for slow operations (> 1000ms)

**Example Log Output**:
```
[PERF] Starting GetAllProducts
[PERF] GetAllProducts completed in 105ms, Memory: 1024 bytes
```

**Real-World Use Cases**:
- SLA monitoring
- Performance tracking
- Bottleneck identification
- Cost analysis (in cloud environments)

### 4. ValidationDecorator

**Purpose**: Validates input before passing to the service

**Features**:
- Validates product name (not empty, min 3 chars)
- Validates price (> 0, ≤ 1,000,000)
- Validates quantity (≥ 0)
- Logs validation errors

**Example Validation Rules**:
```
Create Product:
  - Name: Required, 3+ characters
  - Price: > 0 and ≤ 1,000,000
  - Quantity: ≥ 0

Update Product:
  - ID: > 0
  - Same rules as Create
```

**Real-World Use Cases**:
- Input validation
- Business rule enforcement
- Data consistency
- Security (preventing invalid data)

## API Endpoints

### Products Endpoints

#### Get All Products
```http
GET /api/products
```

**Response**:
```json
{
  "success": true,
  "message": "Retrieved 3 products successfully",
  "data": [
    {
      "id": 1,
      "name": "Laptop",
      "description": "High-performance laptop",
      "price": 999.99,
      "quantity": 10,
      "createdAt": "2024-04-03T10:00:00Z",
      "updatedAt": "2024-04-08T15:30:00Z"
    }
  ],
  "timestamp": "2024-04-10T12:00:00Z"
}
```

**Decorators Applied**: Logging, Caching, Performance Monitoring

---

#### Get Product by ID
```http
GET /api/products/{id}
```

**Response**:
```json
{
  "success": true,
  "message": "Product retrieved successfully",
  "data": {
    "id": 1,
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 999.99,
    "quantity": 10,
    "createdAt": "2024-04-03T10:00:00Z",
    "updatedAt": "2024-04-08T15:30:00Z"
  },
  "timestamp": "2024-04-10T12:00:00Z"
}
```

**Decorators Applied**: Logging, Caching, Performance Monitoring

---

#### Create Product
```http
POST /api/products
Content-Type: application/json

{
  "name": "Monitor",
  "description": "4K Monitor",
  "price": 399.99,
  "quantity": 25
}
```

**Response (201 Created)**:
```json
{
  "success": true,
  "message": "Product created successfully",
  "data": {
    "id": 4,
    "name": "Monitor",
    "description": "4K Monitor",
    "price": 399.99,
    "quantity": 25,
    "createdAt": "2024-04-10T12:00:00Z",
    "updatedAt": "2024-04-10T12:00:00Z"
  },
  "timestamp": "2024-04-10T12:00:00Z"
}
```

**Decorators Applied**: Validation, Logging, Performance Monitoring, Cache Invalidation

**Validation Rules Applied**:
- Name: Required, 3+ characters
- Price: > 0, ≤ 1,000,000
- Quantity: ≥ 0

---

#### Update Product
```http
PUT /api/products/{id}
Content-Type: application/json

{
  "name": "Updated Laptop",
  "description": "Updated description",
  "price": 1099.99,
  "quantity": 5
}
```

**Response**:
```json
{
  "success": true,
  "message": "Product updated successfully",
  "data": { ... },
  "timestamp": "2024-04-10T12:00:00Z"
}
```

**Decorators Applied**: Validation, Logging, Performance Monitoring, Cache Invalidation

---

#### Delete Product
```http
DELETE /api/products/{id}
```

**Response**:
```json
{
  "success": true,
  "message": "Product deleted successfully",
  "data": true,
  "timestamp": "2024-04-10T12:00:00Z"
}
```

**Decorators Applied**: Validation, Logging, Performance Monitoring, Cache Invalidation

---

#### Search Products
```http
GET /api/products/search/{searchTerm}
```

**Example**: `GET /api/products/search/laptop`

**Response**:
```json
{
  "success": true,
  "message": "Found 1 products matching 'laptop'",
  "data": [ ... ],
  "timestamp": "2024-04-10T12:00:00Z"
}
```

**Decorators Applied**: Logging, Performance Monitoring

---

### Decorator Pattern Info Endpoints

#### Get Pattern Information
```http
GET /api/decoratorpattern/info
```

Returns detailed information about the Decorator Pattern implementation.

#### Get Decorators Explanation
```http
GET /api/decoratorpattern/decorators
```

Returns detailed explanation of each decorator and its use cases.

## Dependency Injection Configuration

The decorators are registered in `Program.cs` using a custom extension method:

```csharp
// Step 1: Register the base service
builder.Services.AddScoped<IProductService, ProductService>();

// Step 2-5: Wrap with decorators (order matters - last registered is outermost)
builder.Services.Decorate<IProductService>((inner, sp) => 
    new ValidationDecorator(inner, sp.GetRequiredService<ILogger<ValidationDecorator>>()));

builder.Services.Decorate<IProductService>((inner, sp) => 
    new PerformanceMonitoringDecorator(inner, ...));

builder.Services.Decorate<IProductService>((inner, sp) => 
    new CachingDecorator(inner, ...));

builder.Services.Decorate<IProductService>((inner, sp) => 
    new LoggingDecorator(inner, ...));
```

## Running the API

### Prerequisites
- .NET 10 SDK
- Visual Studio / VS Code

### Run Development Server
```bash
cd DecoratorPattern.API
dotnet run
```

### Access Swagger UI
Navigate to: `https://localhost:5001/swagger`

### Example Requests

**Get all products** (with caching):
```bash
curl -X GET "https://localhost:5001/api/products" -H "accept: application/json"
```

**Create a product** (with validation):
```bash
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"New Product","description":"Desc","price":99.99,"quantity":10}'
```

## Key Benefits Demonstrated

### 1. Separation of Concerns
Each decorator handles one specific responsibility:
- LoggingDecorator → Logging
- CachingDecorator → Caching
- ValidationDecorator → Validation
- PerformanceMonitoringDecorator → Performance monitoring

### 2. Single Responsibility Principle
The core `ProductService` focuses only on business logic, not cross-cutting concerns.

### 3. Open/Closed Principle
- Open for extension: Easy to add new decorators
- Closed for modification: Existing code doesn't need changes

### 4. Reusability
Same decorators can be applied to any implementation of `IProductService`.

### 5. Composability
Decorators can be stacked in any combination without modifying them.

### 6. Testability
Each decorator can be tested independently:

```csharp
[Test]
public void LoggingDecorator_LogsOperations()
{
    var mockService = new Mock<IProductService>();
    var logger = new Mock<ILogger<LoggingDecorator>>();
    
    var decorator = new LoggingDecorator(mockService.Object, logger.Object);
    
    decorator.GetAllProductsAsync();
    
    // Verify logging was called
    logger.Verify(x => x.LogInformation(It.IsAny<string>()), Times.AtLeast(2));
}
```

## Anti-Patterns & When NOT to Use

⚠️ **Don't use Decorator Pattern if**:
- Simple inheritance would suffice
- Only one or two variations needed
- Performance is critical (each decorator adds overhead)
- Debugging is more important than flexibility

## Performance Considerations

- Each decorator adds a small overhead (~0.5-1ms per layer)
- Caching decorator significantly improves read performance
- Suitable for most business applications
- Not suitable for high-frequency trading or real-time systems

## Future Enhancements

1. Add `AuthenticationDecorator` for role-based access control
2. Add `CompressionDecorator` for response compression
3. Add `RateLimitingDecorator` for API throttling
4. Add `CircuitBreakerDecorator` for fault tolerance
5. Add `TransactionDecorator` for ACID compliance

## References

- [Gang of Four Design Patterns](https://en.wikipedia.org/wiki/Design_Patterns)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [ASP.NET Core Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Decorator Pattern Documentation](../../DECORATOR_PATTERN_GUIDE.md)

---

**Version**: 1.0  
**Last Updated**: May 2026
