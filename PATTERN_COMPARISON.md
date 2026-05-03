# Decorator Pattern - Comparison with Alternatives

## When to Use Decorator Pattern vs. Alternatives

| Requirement | Decorator | Inheritance | Strategy | Proxy | Factory |
|---|---|---|---|---|---|
| **Add features dynamically** | ✅ Best | ❌ No | ⚠️ Limited | ⚠️ Limited | ❌ No |
| **Avoid class explosion** | ✅ Best | ❌ Worst | ⚠️ OK | ✅ Good | ⚠️ OK |
| **Compose behaviors** | ✅ Best | ❌ No | ⚠️ Limited | ✅ Good | ✅ Good |
| **Runtime flexibility** | ✅ Excellent | ❌ No | ✅ Good | ⚠️ Limited | ⚠️ Limited |
| **Single Responsibility** | ✅ Great | ⚠️ Hard | ✅ Great | ✅ Great | ✅ Good |
| **Simple implementation** | ⚠️ Medium | ✅ Simple | ✅ Simple | ⚠️ Medium | ⚠️ Medium |
| **Performance** | ⚠️ Overhead | ✅ Best | ✅ Good | ⚠️ Overhead | ✅ Good |
| **Debuggability** | ⚠️ Hard | ✅ Easy | ✅ Easy | ⚠️ Medium | ✅ Easy |

---

## Detailed Comparisons

### Decorator vs. Inheritance

#### Problem: Adding features to classes
```csharp
// Without Decorator (Inheritance - Class Explosion)
public class SimpleCoffee { }
public class CoffeeWithMilk : SimpleCoffee { }
public class CoffeeWithSugar : SimpleCoffee { }
public class CoffeeWithMilkAndSugar : CoffeeWithMilk { } // Problem: must inherit from CoffeeWithMilk
public class CoffeeWithMilkAndSugarAndCream : CoffeeWithMilkAndSugar { } // More nesting!

// With Decorator (Composition - Clean)
public class Coffee { }
public class MilkDecorator : Decorator { }
public class SugarDecorator : Decorator { }
public class CreamDecorator : Decorator { }

var coffee = new SimpleCoffee();
coffee = new MilkDecorator(coffee);
coffee = new SugarDecorator(coffee);
coffee = new CreamDecorator(coffee);
```

#### Comparison Matrix

| Aspect | Inheritance | Decorator |
|--------|-------------|-----------|
| **Class Explosion** | O(2^n) classes for n features | O(n) decorators |
| **Combination** | Manual subclass per combination | Combine at runtime |
| **Feature Addition** | New subclass required | New decorator class |
| **Modification** | May break existing code | Original unchanged |
| **Composition** | Static (compile-time) | Dynamic (runtime) |
| **Flexibility** | Low | High |

#### When to Use Each

**Use Inheritance when**:
- IS-A relationship (not HAS-A)
- Small, fixed set of variations
- Features are always together
- Example: `Dog extends Animal`

**Use Decorator when**:
- HAS-A relationship (composition)
- Many combinations needed
- Features are independent
- Example: Adding features to coffee

---

### Decorator vs. Strategy Pattern

#### Problem: Different algorithms

```csharp
// Strategy: Different algorithms/behaviors
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy { }
public class PayPalPayment : IPaymentStrategy { }
public class CryptoCurrencyPayment : IPaymentStrategy { }

// Decorator: Add behavior to existing behavior
public interface IShoppingCart
{
    void Checkout();
}

public class ShoppingCart : IShoppingCart { }
public class LoggingCartDecorator : IShoppingCart { }
public class ValidationCartDecorator : IShoppingCart { }
```

#### Key Difference

| Strategy | Decorator |
|----------|-----------|
| **Purpose** | Select algorithm | Add features |
| **Relationship** | Encapsulates | Wraps |
| **Composition** | Select one | Stack many |
| **Example** | Choose payment method | Log, validate, cache |

#### When to Use Each

**Use Strategy when**:
- Different algorithms for same task
- Algorithm selection is external
- Algorithms are mutually exclusive
- Example: Different sorting algorithms

**Use Decorator when**:
- Adding features to objects
- Features compose together
- Features are independent
- Example: Adding logging to all methods

---

### Decorator vs. Proxy Pattern

#### Problem: Controlling access or adding behavior

```csharp
// Proxy: Control access to object
public class UserServiceProxy : IUserService
{
    private IUserService _realService;
    
    public User GetUser(int id)
    {
        // Check authorization
        if (!UserHasPermission(id))
            throw new UnauthorizedException();
        
        return _realService.GetUser(id);
    }
}

// Decorator: Add functionality
public class LoggingUserServiceDecorator : IUserService
{
    private IUserService _service;
    
    public User GetUser(int id)
    {
        Logger.Log("Getting user: " + id);
        var user = _service.GetUser(id);
        Logger.Log("Got user: " + user.Name);
        return user;
    }
}
```

#### Key Difference

| Proxy | Decorator |
|-------|-----------|
| **Purpose** | Control access | Add functionality |
| **Intent** | Protect | Enhance |
| **Interface** | Same | Same |
| **Usage** | Transparent | Intentional |

#### When to Use Each

**Use Proxy when**:
- Need to control access
- Lazy initialization
- Access control
- Remote service calls
- Example: Database connection pool

**Use Decorator when**:
- Need to add behavior
- Behavior is orthogonal
- Multiple decorators combine
- Example: Logging, caching, validation

---

### Decorator vs. Factory Pattern

#### Problem: Creating complex objects with various configurations

```csharp
// Factory: Create objects with various combinations
public class CoffeeFactory
{
    public ICoffee CreateSimpleCoffee() => new SimpleCoffee();
    public ICoffee CreateCoffeeWithMilk() => new SimpleCoffee() + MilkDecorator;
    public ICoffee CreateCoffeeWithMilkAndSugar() => // ...
}

// Decorator: Compose objects at runtime
public ICoffee coffee = new SimpleCoffee();
if (wantsMilk) coffee = new MilkDecorator(coffee);
if (wantsSugar) coffee = new SugarDecorator(coffee);
if (wantsCream) coffee = new CreamDecorator(coffee);
```

#### Key Difference

| Factory | Decorator |
|---------|-----------|
| **Purpose** | Create objects | Enhance objects |
| **Timing** | Build object | After object exists |
| **Configuration** | Predefined combinations | Dynamic combinations |
| **Pattern** | Creational | Structural |

#### When to Use Each

**Use Factory when**:
- Complex object creation
- Predefined configurations
- Need to encapsulate creation logic
- Example: Building database connections

**Use Decorator when**:
- Enhancing existing objects
- Dynamic feature addition
- Multiple independent features
- Example: Adding features after creation

---

## Decision Tree

```
Do you want to add FEATURES to existing objects?
│
├─ YES → Decorator Pattern ✅
│        (Logging, caching, validation, etc.)
│
└─ NO
   │
   ├─ Do you need to SELECT ONE algorithm?
   │  │
   │  ├─ YES → Strategy Pattern ✅
   │  │        (Payment methods, sorting, etc.)
   │  │
   │  └─ NO
   │     │
   │     ├─ Do you need to CONTROL ACCESS?
   │     │  │
   │     │  ├─ YES → Proxy Pattern ✅
   │     │  │        (Authorization, lazy loading, etc.)
   │     │  │
   │     │  └─ NO
   │     │     │
   │     │     ├─ Do you have IS-A relationship?
   │     │     │  │
   │     │     │  ├─ YES → Inheritance ✅
   │     │     │  │        (Dog IS-A Animal)
   │     │     │  │
   │     │     │  └─ NO → HAS-A relationship
   │     │     │           → Consider Composition
   │     │     │           → Maybe Decorator Pattern
```

---

## Real-World Scenarios

### Scenario 1: E-Commerce Order Processing

**Problem**: An order goes through validation, logging, payment processing, and shipping calculation. Each can be enabled/disabled.

**Solutions**:

❌ **Wrong: Inheritance**
```csharp
public class Order { }
public class ValidatedOrder : Order { }
public class LoggedOrder : ValidatedOrder { }
public class PaidOrder : LoggedOrder { }
public class ShippedOrder : PaidOrder { }
// Problem: Fixed order, can't skip validation
```

✅ **Right: Decorator**
```csharp
IOrder order = new SimpleOrder(items);
if (needsValidation) order = new ValidationDecorator(order);
if (needsLogging) order = new LoggingDecorator(order);
if (needsPayment) order = new PaymentDecorator(order);
if (needsShipping) order = new ShippingDecorator(order);
```

---

### Scenario 2: File I/O Operations

**Problem**: Reading files may involve decompression, decryption, and caching.

**Solutions**:

❌ **Wrong: Strategy (it's not different algorithms)**
```csharp
public interface IFileReaderStrategy { }
public class CompressedFileReader { }
public class EncryptedFileReader { }
// Problem: Can't combine them
```

✅ **Right: Decorator**
```csharp
IFileReader reader = new FileReader(path);
if (isCompressed) reader = new DecompressionDecorator(reader);
if (isEncrypted) reader = new DecryptionDecorator(reader);
if (shouldCache) reader = new CachingDecorator(reader);

var content = reader.Read(); // All decorators applied
```

---

### Scenario 3: Database Access

**Problem**: Database calls need authorization checks, caching, retry logic, and performance monitoring.

**Solutions**:

❌ **Wrong: Inheritance (too many combinations)**
```csharp
public class UserRepository { }
public class CachedUserRepository : UserRepository { }
public class LoggedCachedUserRepository : CachedUserRepository { }
// Problem: can't just log without caching
```

✅ **Right: Decorator with DI**
```csharp
services.AddScoped<IUserRepository, UserRepository>();
services.Decorate<IUserRepository>((inner, sp) => new AuthorizationDecorator(inner));
services.Decorate<IUserRepository>((inner, sp) => new CachingDecorator(inner, cache));
services.Decorate<IUserRepository>((inner, sp) => new RetryDecorator(inner));
services.Decorate<IUserRepository>((inner, sp) => new MonitoringDecorator(inner));
```

---

### Scenario 4: API Response Handling

**Problem**: Responses need compression, encryption, logging, and validation.

**Solutions**:

❌ **Wrong: Factory with many methods**
```csharp
public class ResponseFactory
{
    public IResponse CreateBasic() => new BasicResponse();
    public IResponse CreateCompressed() => new CompressedResponse();
    public IResponse CreateEncrypted() => new EncryptedResponse();
    public IResponse CreateCompressedAndEncrypted() => new CompressedEncryptedResponse();
    // More combinations...
}
```

✅ **Right: Decorator at middleware level**
```csharp
public class CompressionMiddleware { } // Decorator
public class EncryptionMiddleware { } // Decorator
public class LoggingMiddleware { } // Decorator

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<CompressionMiddleware>();
app.UseMiddleware<EncryptionMiddleware>();
```

---

## Summary Table

```
┌─────────────────┬──────────────┬──────────────┬──────────────┬─────────────┐
│ Pattern         │ Add Features │ Select One   │ Control      │ Create      │
│                 │              │ Algorithm    │ Access       │ Complex     │
├─────────────────┼──────────────┼──────────────┼──────────────┼─────────────┤
│ Decorator       │ ✅ BEST      │ ❌ No        │ ⚠️ Limited   │ ⚠️ Limited  │
│ Inheritance     │ ❌ No        │ ❌ No        │ ❌ No        │ ❌ No       │
│ Strategy        │ ❌ No        │ ✅ BEST      │ ❌ No        │ ❌ No       │
│ Proxy           │ ⚠️ Limited   │ ❌ No        │ ✅ BEST      │ ⚠️ Limited  │
│ Factory         │ ⚠️ Limited   │ ⚠️ Limited   │ ❌ No        │ ✅ BEST     │
└─────────────────┴──────────────┴──────────────┴──────────────┴─────────────┘
```

---

## Combination Strategies

### Using Multiple Patterns Together

#### Decorator + Factory
```csharp
// Factory creates the base object
// Decorator adds features
public class CoffeeFactory
{
    public static ICoffee CreatePremiumCoffee()
    {
        var coffee = new PremiumCoffee();
        coffee = new MilkDecorator(coffee);
        coffee = new HoneyDecorator(coffee);
        return coffee;
    }
}
```

#### Decorator + Strategy
```csharp
// Decorator adds behavior
// Strategy changes algorithm
var payment = new PaymentService();
payment = new LoggingDecorator(payment);
payment = new ValidationDecorator(payment);

var strategy = new CreditCardPaymentStrategy();
payment.Process(strategy); // Logged and validated
```

#### Decorator + Proxy
```csharp
// Proxy controls access
// Decorator adds features
var service = new UserService();
service = new AuthorizationProxy(service);
service = new LoggingDecorator(service);
```

---

## Conclusion

- **Use Decorator** when you need to add independent features that can be combined
- **Use Inheritance** when you have IS-A relationships
- **Use Strategy** when you need to swap algorithms
- **Use Proxy** when you need to control access
- **Use Factory** when object creation is complex

The key is understanding the **intent** of each pattern and choosing the one that best solves your specific problem.

---

**Remember**: The best pattern is the one that makes your code clearer, more maintainable, and easier to test! 🎯
