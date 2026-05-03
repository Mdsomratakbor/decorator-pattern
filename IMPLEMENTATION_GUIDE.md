# Decorator Pattern - Implementation Guide

## Table of Contents
1. [How to Implement Decorators](#how-to-implement-decorators)
2. [Step-by-Step Guide](#step-by-step-guide)
3. [Common Patterns](#common-patterns)
4. [Testing Decorators](#testing-decorators)
5. [Performance Considerations](#performance-considerations)
6. [Extending the Project](#extending-the-project)

---

## How to Implement Decorators

### Core Components

Every decorator implementation requires:

1. **Component Interface** - Defines what can be decorated
2. **Concrete Component** - The actual implementation
3. **Abstract Decorator** - Base class for all decorators
4. **Concrete Decorators** - Specific implementations

### Template Pattern

```csharp
// 1. Interface
public interface IComponent
{
    void Operation();
}

// 2. Concrete Component
public class ConcreteComponent : IComponent
{
    public void Operation() => Console.WriteLine("Base operation");
}

// 3. Abstract Decorator
public abstract class Decorator : IComponent
{
    protected IComponent _component;
    
    protected Decorator(IComponent component)
    {
        _component = component;
    }
    
    public virtual void Operation()
    {
        _component.Operation();
    }
}

// 4. Concrete Decorators
public class ConcreteDecoratorA : Decorator
{
    public ConcreteDecoratorA(IComponent component) : base(component) { }
    
    public override void Operation()
    {
        Console.WriteLine("Before A");
        base.Operation();
        Console.WriteLine("After A");
    }
}
```

---

## Step-by-Step Guide

### Step 1: Define the Interface

```csharp
/// <summary>
/// Defines the contract for components and decorators
/// </summary>
public interface IDataService
{
    Task<T> GetDataAsync<T>(string key);
    Task SaveDataAsync<T>(string key, T data);
}
```

**Why**: 
- Allows both components and decorators to implement the same interface
- Enables runtime decoration
- Follows Liskov Substitution Principle

---

### Step 2: Create Concrete Component

```csharp
/// <summary>
/// Core implementation with business logic
/// </summary>
public class DatabaseDataService : IDataService
{
    public async Task<T> GetDataAsync<T>(string key)
    {
        // Actual database call
        await Task.Delay(100);
        return default(T);
    }
    
    public async Task SaveDataAsync<T>(string key, T data)
    {
        // Actual database save
        await Task.Delay(100);
    }
}
```

**Why**:
- Contains only core business logic
- Unaware of decorators
- Easy to test in isolation

---

### Step 3: Create Abstract Decorator

```csharp
/// <summary>
/// Base class for all data service decorators
/// </summary>
public abstract class DataServiceDecorator : IDataService
{
    /// <summary>
    /// Holds reference to wrapped service
    /// </summary>
    protected readonly IDataService _innerService;
    
    protected DataServiceDecorator(IDataService innerService)
    {
        _innerService = innerService;
    }
    
    /// <summary>
    /// Default: delegate to wrapped service
    /// Subclasses override to add behavior
    /// </summary>
    public virtual async Task<T> GetDataAsync<T>(string key)
    {
        return await _innerService.GetDataAsync<T>(key);
    }
    
    public virtual async Task SaveDataAsync<T>(string key, T data)
    {
        await _innerService.SaveDataAsync(key, data);
    }
}
```

**Why**:
- Centralizes common decorator logic
- Ensures consistent interface
- Reduces code duplication

---

### Step 4: Create Concrete Decorators

```csharp
/// <summary>
/// Decorator that adds caching
/// </summary>
public class CachingDecorator : DataServiceDecorator
{
    private readonly IMemoryCache _cache;
    
    public CachingDecorator(IDataService innerService, IMemoryCache cache)
        : base(innerService)
    {
        _cache = cache;
    }
    
    public override async Task<T> GetDataAsync<T>(string key)
    {
        // Check cache first
        if (_cache.TryGetValue(key, out T cachedValue))
        {
            return cachedValue;
        }
        
        // Get from wrapped service
        var value = await base.GetDataAsync<T>(key);
        
        // Store in cache
        _cache.Set(key, value, TimeSpan.FromMinutes(5));
        
        return value;
    }
    
    public override async Task SaveDataAsync<T>(string key, T data)
    {
        // Invalidate cache
        _cache.Remove(key);
        
        // Save to wrapped service
        await base.SaveDataAsync(key, data);
    }
}

/// <summary>
/// Decorator that adds logging
/// </summary>
public class LoggingDecorator : DataServiceDecorator
{
    private readonly ILogger _logger;
    
    public LoggingDecorator(IDataService innerService, ILogger logger)
        : base(innerService)
    {
        _logger = logger;
    }
    
    public override async Task<T> GetDataAsync<T>(string key)
    {
        _logger.LogInformation($"Getting {key}");
        var result = await base.GetDataAsync<T>(key);
        _logger.LogInformation($"Got {key}: {result}");
        return result;
    }
    
    public override async Task SaveDataAsync<T>(string key, T data)
    {
        _logger.LogInformation($"Saving {key}: {data}");
        await base.SaveDataAsync(key, data);
        _logger.LogInformation($"Saved {key}");
    }
}
```

**Why**:
- Each decorator has ONE responsibility
- Follows Single Responsibility Principle
- Easy to test and maintain

---

### Step 5: Compose Decorators

#### Using Constructor Chaining

```csharp
// Create the service with decorators
IDataService service = new DatabaseDataService();
service = new CachingDecorator(service, cache);
service = new LoggingDecorator(service, logger);

// Use it
var data = await service.GetDataAsync<string>("key1");
```

#### Using Dependency Injection (Recommended)

```csharp
// In Program.cs or Startup.cs
services.AddScoped<IDataService, DatabaseDataService>();

// Wrap with decorators
services.Decorate<IDataService>((inner, sp) =>
    new CachingDecorator(inner, sp.GetRequiredService<IMemoryCache>()));

services.Decorate<IDataService>((inner, sp) =>
    new LoggingDecorator(inner, sp.GetRequiredService<ILogger>()));
```

---

## Common Patterns

### Pattern 1: Conditional Decorator

```csharp
public class ConditionalCachingDecorator : DataServiceDecorator
{
    private readonly IMemoryCache _cache;
    private readonly bool _enableCaching;
    
    public ConditionalCachingDecorator(IDataService inner, IMemoryCache cache, bool enableCaching)
        : base(inner)
    {
        _cache = cache;
        _enableCaching = enableCaching;
    }
    
    public override async Task<T> GetDataAsync<T>(string key)
    {
        if (!_enableCaching)
            return await base.GetDataAsync<T>(key);
        
        // Caching logic...
        if (_cache.TryGetValue(key, out T value))
            return value;
        
        var result = await base.GetDataAsync<T>(key);
        _cache.Set(key, result);
        return result;
    }
}
```

### Pattern 2: Decorator with Configuration

```csharp
public class ConfigurableDecorator : DataServiceDecorator
{
    private readonly DecoratorConfig _config;
    
    public ConfigurableDecorator(IDataService inner, IOptions<DecoratorConfig> config)
        : base(inner)
    {
        _config = config.Value;
    }
    
    public override async Task<T> GetDataAsync<T>(string key)
    {
        if (_config.Enabled)
        {
            // Apply decorator logic based on config
        }
        return await base.GetDataAsync<T>(key);
    }
}

public class DecoratorConfig
{
    public bool Enabled { get; set; }
    public int CacheDurationMinutes { get; set; }
    public bool LogDetails { get; set; }
}
```

### Pattern 3: Decorator Chain with Factory

```csharp
public static class DataServiceFactory
{
    public static IDataService CreateDefaultService(IServiceProvider provider)
    {
        IDataService service = new DatabaseDataService();
        
        // Apply decorators in order
        if (provider.GetRequiredService<IOptions<AppSettings>>().Value.EnableCaching)
            service = new CachingDecorator(service, 
                provider.GetRequiredService<IMemoryCache>());
        
        if (provider.GetRequiredService<IOptions<AppSettings>>().Value.EnableLogging)
            service = new LoggingDecorator(service, 
                provider.GetRequiredService<ILogger>());
        
        if (provider.GetRequiredService<IOptions<AppSettings>>().Value.EnableValidation)
            service = new ValidationDecorator(service);
        
        return service;
    }
}
```

### Pattern 4: Decorator with Performance Metrics

```csharp
public class MetricsDecorator : DataServiceDecorator
{
    private readonly IMetricsCollector _metrics;
    
    public override async Task<T> GetDataAsync<T>(string key)
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var result = await base.GetDataAsync<T>(key);
            return result;
        }
        finally
        {
            stopwatch.Stop();
            _metrics.RecordOperation("GetDataAsync", stopwatch.ElapsedMilliseconds);
        }
    }
}
```

### Pattern 5: Decorator with Retry Logic

```csharp
public class RetryDecorator : DataServiceDecorator
{
    private readonly int _maxRetries;
    
    public override async Task<T> GetDataAsync<T>(string key)
    {
        int attempts = 0;
        while (attempts < _maxRetries)
        {
            try
            {
                return await base.GetDataAsync<T>(key);
            }
            catch (Exception ex) when (attempts < _maxRetries - 1)
            {
                attempts++;
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempts)));
            }
        }
        
        throw new OperationException("Max retries exceeded");
    }
}
```

---

## Testing Decorators

### Unit Testing Decorators

```csharp
[TestFixture]
public class CachingDecoratorTests
{
    [Test]
    public async Task GetDataAsync_FirstCall_CallsInnerService()
    {
        // Arrange
        var mockService = new Mock<IDataService>();
        var cache = new MemoryCache(new MemoryCacheOptions());
        var decorator = new CachingDecorator(mockService.Object, cache);
        
        mockService.Setup(x => x.GetDataAsync<string>("key"))
            .ReturnsAsync("value");
        
        // Act
        var result = await decorator.GetDataAsync<string>("key");
        
        // Assert
        Assert.AreEqual("value", result);
        mockService.Verify(x => x.GetDataAsync<string>("key"), Times.Once);
    }
    
    [Test]
    public async Task GetDataAsync_SecondCall_ReturnsCached()
    {
        // Arrange
        var mockService = new Mock<IDataService>();
        var cache = new MemoryCache(new MemoryCacheOptions());
        var decorator = new CachingDecorator(mockService.Object, cache);
        
        mockService.Setup(x => x.GetDataAsync<string>("key"))
            .ReturnsAsync("value");
        
        // Act
        await decorator.GetDataAsync<string>("key");
        var result2 = await decorator.GetDataAsync<string>("key");
        
        // Assert
        Assert.AreEqual("value", result2);
        mockService.Verify(x => x.GetDataAsync<string>("key"), Times.Once); // Only called once
    }
    
    [Test]
    public async Task SaveDataAsync_InvalidatesCache()
    {
        // Arrange
        var mockService = new Mock<IDataService>();
        var cache = new MemoryCache(new MemoryCacheOptions());
        var decorator = new CachingDecorator(mockService.Object, cache);
        
        mockService.Setup(x => x.GetDataAsync<string>("key"))
            .ReturnsAsync("value");
        mockService.Setup(x => x.SaveDataAsync("key", "newvalue"))
            .Returns(Task.CompletedTask);
        
        // Act
        await decorator.GetDataAsync<string>("key");
        await decorator.SaveDataAsync("key", "newvalue");
        await decorator.GetDataAsync<string>("key");
        
        // Assert
        mockService.Verify(x => x.GetDataAsync<string>("key"), Times.Exactly(2)); // Called twice now
    }
}
```

### Integration Testing

```csharp
[TestFixture]
public class DecoratorIntegrationTests
{
    [Test]
    public async Task CompleteDecoratorStack_WorksCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped<IDataService, DatabaseDataService>();
        services.AddMemoryCache();
        
        services.Decorate<IDataService>((inner, sp) =>
            new CachingDecorator(inner, sp.GetRequiredService<IMemoryCache>()));
        services.Decorate<IDataService>((inner, sp) =>
            new LoggingDecorator(inner, new TestLogger()));
        
        var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IDataService>();
        
        // Act
        var result1 = await service.GetDataAsync<string>("key");
        var result2 = await service.GetDataAsync<string>("key"); // From cache
        
        // Assert
        Assert.AreEqual(result1, result2);
    }
}
```

---

## Performance Considerations

### Benchmarking Example

```csharp
[MemoryDiagnoser]
public class DecoratorBenchmarks
{
    private IDataService _service;
    private IDataService _cachedService;
    
    [GlobalSetup]
    public void Setup()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var baseService = new DatabaseDataService();
        
        _service = baseService;
        _cachedService = new CachingDecorator(baseService, cache);
    }
    
    [Benchmark]
    public async Task GetData_NoDecorators()
    {
        await _service.GetDataAsync<string>("key");
    }
    
    [Benchmark]
    public async Task GetData_WithCaching_FirstCall()
    {
        await _cachedService.GetDataAsync<string>("unique_key");
    }
    
    [Benchmark]
    public async Task GetData_WithCaching_Cached()
    {
        await _cachedService.GetDataAsync<string>("key");
    }
}
```

**Expected Results**:
- No decorators: ~100ms
- With caching (first call): ~101ms (1ms overhead)
- With caching (cached): ~0.1ms

---

## Extending the Project

### Add a New Decorator to Console App

**Step 1**: Create a new decorator class
```csharp
public class UppercaseDecorator : TextDecorator
{
    public UppercaseDecorator(IText text) : base(text) { }
    
    public override string GetFormattedText()
    {
        return base.GetFormattedText().ToUpper();
    }
}
```

**Step 2**: Add to example
```csharp
IText text = new PlainText("hello");
text = new UppercaseDecorator(text);
Console.WriteLine(text.GetFormattedText()); // "HELLO"
```

### Add a New Decorator to API

**Step 1**: Create decorator
```csharp
public class EncryptionDecorator : ServiceDecoratorBase
{
    // Implementation...
}
```

**Step 2**: Register in Program.cs
```csharp
builder.Services.Decorate<IProductService>((inner, sp) =>
    new EncryptionDecorator(inner));
```

### Ideas for New Decorators

**Console App**:
- RotateDecorator - Rotate text
- ReverseDecorator - Reverse text
- ColorDecorator - Add color codes

**API**:
- AuthenticationDecorator - Check authorization
- RateLimitingDecorator - Limit requests
- CompressionDecorator - Compress responses
- CircuitBreakerDecorator - Handle failures
- AuditDecorator - Track changes

---

## Best Practices Summary

✅ **DO**:
- Keep decorators focused (one responsibility)
- Use abstract base class for common logic
- Register decorators in dependency injection
- Test decorators independently
- Document decorator stack order
- Use factory pattern for complex composition

❌ **DON'T**:
- Create decorators with multiple responsibilities
- Skip tests for decorators
- Change decorator order without understanding impact
- Make decorators stateful
- Ignore performance overhead
- Use decorators when simple inheritance would suffice

---

**Happy Decorating! 🎉**
