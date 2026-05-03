"# Decorator Pattern - Complete .NET 10 Implementation

A comprehensive guide and implementation of the **Decorator Pattern** with both console and API applications demonstrating real-world usage.

## 📚 Documentation

### Comprehensive Guide
Start here to understand the Decorator Pattern:
- **[DECORATOR_PATTERN_GUIDE.md](DECORATOR_PATTERN_GUIDE.md)** - Complete theoretical guide covering:
  - What is the Decorator Pattern?
  - How does it work?
  - When to use it
  - Real-world examples
  - SOLID principles
  - Best practices
  - Advantages and disadvantages

### Project Documentation
- **Console App**: Examples demonstrating the pattern with simple cases
- **API Project**: [API_EXAMPLES.md](DecoratorPattern.API/Documentation/API_EXAMPLES.md) - Real-world HTTP request handling

## 🎯 Quick Start

### Prerequisites
- .NET 10 SDK
- Visual Studio 2022, VS Code, or Rider

### Run Console Application

```bash
cd DecoratorPattern.ConsoleApp
dotnet run
```

**Features**:
1. **Basic Decorator Pattern** - Simple component decoration
2. **Coffee Shop Example** - Classic real-world scenario
3. **Text Formatting Example** - Dynamic text formatting

### Run API Application

```bash
cd DecoratorPattern.API
dotnet run
```

**Access**:
- API: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

**Features**:
- Real-world service decoration
- Logging, caching, validation, performance monitoring
- REST endpoints for product management

## 📁 Project Structure

```
Decorator-Pattern/
├── DECORATOR_PATTERN_GUIDE.md              # Comprehensive guide
├── README.md                                # This file
├── DecoratorPattern.sln                    # Solution file
│
├── DecoratorPattern.ConsoleApp/            # Console Application
│   ├── Program.cs
│   ├── Examples/
│   │   ├── BasicDecorator/                 # Basic pattern demo
│   │   ├── CoffeeShop/                     # Classic example
│   │   └── TextFormatting/                 # Text formatting example
│   └── Utils/
│
└── DecoratorPattern.API/                   # ASP.NET Core API
    ├── Program.cs
    ├── Controllers/
    │   ├── ProductsController.cs           # REST endpoints
    │   └── DecoratorPatternController.cs   # Pattern info endpoints
    ├── Services/
    │   ├── IProductService.cs
    │   ├── ProductService.cs               # Core service
    │   ├── Decorators/
    │   │   ├── ServiceDecoratorBase.cs     # Base decorator
    │   │   ├── LoggingDecorator.cs         # Adds logging
    │   │   ├── CachingDecorator.cs         # Adds caching
    │   │   ├── ValidationDecorator.cs      # Adds validation
    │   │   └── PerformanceMonitoringDecorator.cs  # Adds monitoring
    │   └── ServiceCollectionExtensions.cs  # DI helpers
    ├── Models/
    │   ├── Product.cs
    │   ├── ApiResponse.cs
    │   └── PerformanceMetrics.cs
    ├── Documentation/
    │   └── API_EXAMPLES.md                 # API documentation
    └── appsettings.json
```

## 🎓 Learning Outcomes

After going through this project, you'll understand:

✅ What the Decorator Pattern is and when to use it  
✅ How to implement decorators in C#  
✅ Real-world applications (logging, caching, validation)  
✅ How to compose decorators with dependency injection  
✅ SOLID principles in practice  
✅ Best practices for maintainable code  

## 🔍 Console Application Examples

### 1. Basic Decorator Pattern
Demonstrates the core concept of decorators wrapping components.

**Example Output**:
```
SCENARIO 1: Component without decorators
ConcreteComponent: Base Operation with value 5
Final Value: 5

SCENARIO 2: Component wrapped with DecoratorA
ConcreteComponent: Base Operation with value 5
DecoratorA transforms 5 → 10
Final Value: 10

SCENARIO 3: Component wrapped with DecoratorA → DecoratorB
ConcreteComponent: Base Operation with value 5
DecoratorA transforms 5 → 10
DecoratorB transforms 10 → 20
Final Value: 20
```

### 2. Coffee Shop Example
Real-world scenario of building different coffee combinations.

**Example Combinations**:
- Simple Coffee: $5.00
- Coffee + Milk: $5.50
- Coffee + Milk + Sugar: $5.75
- Coffee + Milk + Sugar + Whipped Cream: $6.75

### 3. Text Formatting Example
Dynamic text formatting using decorators.

**Example Output**:
- Plain: `Hello World`
- Bold: `**Hello World**`
- Bold + Italic: `_**Hello World**_`
- Bold + Italic + Underline: `~_**Hello World**_~`

## 🌐 API Examples

### Service Decoration Stack

The API demonstrates how decorators wrap the core service:

```
Request
  ↓ LoggingDecorator (logs all operations)
  ↓ CachingDecorator (caches read operations)
  ↓ PerformanceMonitoringDecorator (measures execution time)
  ↓ ValidationDecorator (validates input)
  ↓ ProductService (core business logic)
  ↓
Response
```

### Key Endpoints

```
GET    /api/products                  # Get all (cached)
GET    /api/products/{id}             # Get one (cached)
POST   /api/products                  # Create (validates)
PUT    /api/products/{id}             # Update (validates, invalidates cache)
DELETE /api/products/{id}             # Delete (validates, invalidates cache)
GET    /api/products/search/{term}    # Search
GET    /api/decoratorpattern/info     # Pattern info
GET    /api/decoratorpattern/decorators # Decorators explanation
```

## 🏛️ Design Principles Demonstrated

### Single Responsibility Principle (SRP)
Each decorator has one specific responsibility:
- LoggingDecorator → Logging
- CachingDecorator → Caching
- ValidationDecorator → Validation
- PerformanceMonitoringDecorator → Monitoring

### Open/Closed Principle (OCP)
- **Open for extension**: Add new decorators without modifying existing ones
- **Closed for modification**: Existing code remains unchanged

### Dependency Inversion Principle (DIP)
- Depend on `IProductService` interface, not concrete implementations
- Decorators implement the same interface

### Liskov Substitution Principle (LSP)
- Decorators can substitute for the original service
- Client code doesn't need to know about decorators

## 🚀 Real-World Use Cases

1. **Logging**: Track all operations for auditing
2. **Caching**: Improve performance by caching results
3. **Validation**: Enforce business rules before processing
4. **Performance Monitoring**: Track execution times and alerts
5. **Authentication**: Add security checks
6. **Rate Limiting**: Control API usage
7. **Compression**: Reduce payload size
8. **Encryption**: Protect sensitive data
9. **Retry Logic**: Handle transient failures
10. **Circuit Breaking**: Prevent cascading failures

## ✨ Advantages

- ✅ Flexible and dynamic composition
- ✅ No class explosion
- ✅ Separation of concerns
- ✅ Reusable decorators
- ✅ Easy to test
- ✅ Follows SOLID principles
- ✅ Runtime configuration

## ⚠️ Disadvantages

- ⚠️ Increased complexity
- ⚠️ Deeper call stacks (harder to debug)
- ⚠️ Performance overhead per decorator
- ⚠️ Order dependency can cause subtle bugs
- ⚠️ Identity issues (`GetType()` returns decorator, not original)

## 📖 References

- Gang of Four Design Patterns
- SOLID Principles
- Head First Design Patterns
- Clean Architecture by Robert C. Martin
- ASP.NET Core Documentation

## 🤝 Contributing

This is a learning project. Feel free to:
- Add more decorators
- Create additional examples
- Improve documentation
- Share feedback

## 📄 License

Educational purposes

## 🎉 Key Takeaways

1. **Decorator Pattern solves the "class explosion" problem**
   - Instead of `CoffeeWithMilk`, `CoffeeWithMilkAndSugar`, etc.
   - Use: `new MilkDecorator(new SugarDecorator(new Coffee()))`

2. **It enables runtime flexibility**
   - Decide which decorators to apply based on conditions
   - No need to subclass ahead of time

3. **It follows SOLID principles**
   - Single Responsibility
   - Open/Closed Principle
   - Liskov Substitution
   - Dependency Inversion

4. **It's practical for enterprise applications**
   - Logging, caching, validation are common needs
   - Decorators provide a clean way to implement cross-cutting concerns

5. **Composition over inheritance**
   - More flexible than subclassing
   - Easier to maintain and test

---

**Version**: 1.0  
**Last Updated**: May 2026  
**Framework**: .NET 10" 
