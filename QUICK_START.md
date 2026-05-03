# Quick Start Guide - Decorator Pattern Project

## 🚀 5-Minute Setup

### Step 1: Clone or Navigate to Project
```bash
cd Decorator-Pattern
```

### Step 2: View the Solution
```bash
dotnet sln Decorator-Pattern.sln
```

### Step 3a: Run Console Application (Interactive Demo)

**Option A: Using Terminal**
```bash
cd DecoratorPattern.ConsoleApp
dotnet run
```

**Option B: Using Visual Studio**
1. Open `DecoratorPattern.sln` in Visual Studio
2. Right-click on `DecoratorPattern.ConsoleApp` project
3. Select "Set as Startup Project"
4. Press F5 or click Start

**What to Expect**:
- Interactive menu with 3 examples
- Run each example to see decorators in action
- Press any key to return to menu

### Step 3b: Run API Application (REST API)

**Option A: Using Terminal**
```bash
cd DecoratorPattern.API
dotnet run
```

Output:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
      Now listening on: http://localhost:5000
```

**Option B: Using Visual Studio**
1. Set `DecoratorPattern.API` as Startup Project
2. Press F5

### Step 4: Access API

**Via Swagger UI** (recommended for testing):
- Open: https://localhost:5001/swagger
- Try out endpoints directly in browser

**Via curl**:
```bash
# Get all products (will be cached)
curl -X GET "https://localhost:5001/api/products" -k

# Get product info about decorators
curl -X GET "https://localhost:5001/api/decoratorpattern/info" -k

# Create a product
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"Test Product","description":"Test","price":99.99,"quantity":10}' -k
```

## 📚 Understanding the Code

### Console App - 3 Examples

#### Example 1: Basic Decorator (Simplest)
**File**: `DecoratorPattern.ConsoleApp/Examples/BasicDecorator/`

**What it shows**:
- How decorators wrap components
- How each decorator adds behavior
- Order dependency

**Code flow**:
```csharp
var component = new ConcreteComponent();              // value = 5
component = new ConcreteDecoratorA(component);        // multiply by 2 = 10
component = new ConcreteDecoratorB(component);        // add 10 = 20
Console.WriteLine(component.GetValue());              // 20
```

#### Example 2: Coffee Shop (Real-world)
**File**: `DecoratorPattern.ConsoleApp/Examples/CoffeeShop/`

**What it shows**:
- Building different combinations dynamically
- Calculating costs compositionally
- Real-world domain (e-commerce)

**Code flow**:
```csharp
var coffee = new SimpleCoffee();
coffee = new MilkDecorator(coffee);           // +$0.50
coffee = new SugarDecorator(coffee);          // +$0.25
coffee = new WhippedCreamDecorator(coffee);   // +$1.00
Console.WriteLine(coffee.GetCost());          // $6.75
```

#### Example 3: Text Formatting (Visual)
**File**: `DecoratorPattern.ConsoleApp/Examples/TextFormatting/`

**What it shows**:
- Stacking multiple decorators
- Visible transformation
- Easy to add new formatting

**Code flow**:
```csharp
var text = new PlainText("Hello");
text = new BoldDecorator(text);      // "**Hello**"
text = new ItalicDecorator(text);    // "_**Hello**_"
```

### API - Real-World Scenario

#### Service Stack
**File**: `DecoratorPattern.API/Program.cs`

Shows how to compose decorators using dependency injection:

```csharp
// Base service
builder.Services.AddScoped<IProductService, ProductService>();

// Stack decorators (order: last registered = outermost)
builder.Services.Decorate<IProductService>(...ValidationDecorator);
builder.Services.Decorate<IProductService>(...PerformanceMonitoringDecorator);
builder.Services.Decorate<IProductService>(...CachingDecorator);
builder.Services.Decorate<IProductService>(...LoggingDecorator);
```

#### Decorator Details

1. **LoggingDecorator** (`LoggingDecorator.cs`)
   - Logs every operation entry/exit
   - Shows: method name, parameters, results

2. **CachingDecorator** (`CachingDecorator.cs`)
   - Caches GET results for 5 minutes
   - Invalidates cache on CREATE/UPDATE/DELETE
   - Shows: cache hits vs misses

3. **ValidationDecorator** (`ValidationDecorator.cs`)
   - Validates before CREATE/UPDATE
   - Rules: name length, price range, quantity
   - Shows: input validation pattern

4. **PerformanceMonitoringDecorator** (`PerformanceMonitoringDecorator.cs`)
   - Measures execution time
   - Warns if operation > 1000ms
   - Shows: performance monitoring pattern

## 🧪 Testing the Decorators

### Console App - Manual Testing

1. **Run Example 1**: See basic decoration in action
2. **Run Example 2**: See practical real-world example
3. **Run Example 3**: See visual transformation

### API - Testing with Swagger

1. **Start API**: `dotnet run` in `DecoratorPattern.API`
2. **Open**: https://localhost:5001/swagger
3. **Try endpoints**:

   **Test Caching**:
   - Call `GET /api/products` → Look at response time (e.g., 105ms)
   - Call again immediately → Faster (< 5ms, cached)
   - Wait 5 minutes → Slow again (cache expired)

   **Test Validation**:
   - Call `POST /api/products` with:
     ```json
     {"name":"Test","price":-10,"quantity":5}
     ```
   - You'll get validation error (price must be > 0)

   **Test Logging**:
   - Check console output for [LOG] markers
   - Shows: what operation, with what parameters, result

   **Test Performance Monitoring**:
   - Check console for [PERF] markers
   - Shows: execution time in milliseconds

### API - Testing with curl

```bash
# Test 1: Get all products (observe cache)
curl -i -X GET "https://localhost:5001/api/products" -k --silent

# Test 2: Create with validation error
curl -i -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"X","price":999999999,"quantity":5}' -k --silent

# Test 3: Create valid product
curl -i -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"Monitor","price":299.99,"quantity":15}' -k --silent

# Test 4: Get decorator info
curl -i -X GET "https://localhost:5001/api/decoratorpattern/info" -k --silent
```

## 📖 Reading the Code

### Best Order for Understanding

1. **Start here**: `DECORATOR_PATTERN_GUIDE.md` (5 min read)
2. **Simple example**: `BasicDecorator` example in console app (5 min)
3. **Practical example**: `CoffeeShop` example in console app (5 min)
4. **Run console app**: Execute and see examples (5 min)
5. **API structure**: Review `Program.cs` decorator composition (10 min)
6. **API endpoints**: Test using Swagger (10 min)
7. **Deep dive**: Read decorator implementations (20 min)

### Key Files to Understand

```
Console App:
├── BasicDecorator/IComponent.cs              ← Start: interface definition
├── BasicDecorator/ConcreteComponent.cs       ← Core implementation
├── BasicDecorator/Decorator.cs               ← Abstract decorator
├── BasicDecorator/ConcreteDecoratorA/B.cs    ← Specific decorators
└── BasicDecorator/Example.cs                 ← Example usage

API:
├── Services/IProductService.cs               ← Service interface
├── Services/ProductService.cs                ← Core implementation
├── Services/Decorators/ServiceDecoratorBase.cs ← Abstract decorator
├── Services/Decorators/LoggingDecorator.cs   ← Specific decorators
└── Program.cs                                ← Composition root
```

## 🐛 Troubleshooting

### Console App Won't Run
```bash
# Check .NET version
dotnet --version  # Should be 10.x

# Restore packages
dotnet restore

# Clean and rebuild
dotnet clean
dotnet build

# Run
dotnet run
```

### API Won't Start on Port 5001
```bash
# Check if port is in use
netstat -ano | findstr :5001

# Either:
# 1. Kill the process using the port
# 2. Use different port: dotnet run --urls="https://localhost:5002"
```

### Swagger UI Not Loading
```bash
# Clear browser cache (Ctrl+Shift+Delete)
# Try incognito/private window
# Try different browser
```

### HTTPS Certificate Error
```bash
# Trust development certificate
dotnet dev-certs https --trust
```

## 🎯 Next Steps

1. **Run console app** - See all 3 examples
2. **Read guide** - Understand the theory
3. **Run API** - See real-world application
4. **Test with Swagger** - Experiment with decorators
5. **Review code** - Understand implementation
6. **Create new decorator** - Add your own decorator
7. **Combine patterns** - Mix with other patterns

## 💡 Tips & Tricks

### To Add a New Decorator

1. Create new file: `DecoratorPattern.API/Services/Decorators/NewDecorator.cs`
2. Inherit from `ServiceDecoratorBase`
3. Override methods you want to decorate
4. Register in `Program.cs` using `.Decorate<IProductService>()`

### To Add a New Console Example

1. Create directory: `DecoratorPattern.ConsoleApp/Examples/YourExample/`
2. Create interface, component, decorators
3. Create `Example.cs` with static `Run()` method
4. Add to menu in `Program.cs`

### To Run Both Projects Simultaneously

**In Visual Studio**:
1. Right-click solution
2. Select "Set Startup Projects"
3. Choose "Multiple startup projects"
4. Set both console app and API to "Start"

## 📞 Questions?

Refer to:
- `DECORATOR_PATTERN_GUIDE.md` - Comprehensive theory
- `DecoratorPattern.API/Documentation/API_EXAMPLES.md` - API details
- Code comments in example files
- ReadMe in each project

---

**Happy Learning! 🚀**
