using DecoratorPattern.API.Services;
using DecoratorPattern.API.Services.Decorators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add memory cache
builder.Services.AddMemoryCache();

// Add logging
builder.Services.AddLogging();

// Register the service with decorators
// This demonstrates how the Decorator Pattern is applied using Dependency Injection
// The decorators are stacked in order - each wraps the one below it
//
// Execution flow:
// 1. Request enters LoggingDecorator
// 2. LoggingDecorator delegates to CachingDecorator
// 3. CachingDecorator checks cache, delegates to PerformanceMonitoringDecorator
// 4. PerformanceMonitoringDecorator measures time, delegates to ValidationDecorator
// 5. ValidationDecorator validates, delegates to ProductService (the actual implementation)

// Step 1: Create the base service
builder.Services.AddScoped<IProductService, ProductService>();

// Step 2: Wrap with ValidationDecorator
builder.Services.Decorate<IProductService>((inner, sp) => 
    new ValidationDecorator(inner, sp.GetRequiredService<ILogger<ValidationDecorator>>()));

// Step 3: Wrap with PerformanceMonitoringDecorator
builder.Services.Decorate<IProductService>((inner, sp) => 
    new PerformanceMonitoringDecorator(inner, sp.GetRequiredService<ILogger<PerformanceMonitoringDecorator>>()));

// Step 4: Wrap with CachingDecorator
builder.Services.Decorate<IProductService>((inner, sp) => 
    new CachingDecorator(inner, sp.GetRequiredService<IMemoryCache>(), 
        sp.GetRequiredService<ILogger<CachingDecorator>>()));

// Step 5: Wrap with LoggingDecorator (outermost - first to process)
builder.Services.Decorate<IProductService>((inner, sp) => 
    new LoggingDecorator(inner, sp.GetRequiredService<ILogger<LoggingDecorator>>()));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
