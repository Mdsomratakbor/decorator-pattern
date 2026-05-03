# Decorator Pattern - Complete Guide

## Table of Contents
1. [What is the Decorator Pattern?](#what-is-the-decorator-pattern)
2. [How Does It Work?](#how-does-it-work)
3. [Key Concepts](#key-concepts)
4. [When to Use Decorator Pattern](#when-to-use-decorator-pattern)
5. [Real-World Examples](#real-world-examples)
6. [Advantages](#advantages)
7. [Disadvantages](#disadvantages)
8. [Design Principles](#design-principles)
9. [Best Practices](#best-practices)
10. [Project Structure](#project-structure)

---

## What is the Decorator Pattern?

The **Decorator Pattern** is a **Structural Design Pattern** that allows you to attach additional responsibilities to an object **dynamically**. It provides a flexible alternative to subclassing for extending functionality.

### Problem It Solves
Imagine you have a `Coffee` class. If you need to add milk, add sugar, add whipped cream, or any combination thereof, you could create subclasses like:
- `CoffeeWithMilk`
- `CoffeeWithSugar`
- `CoffeeWithMilkAndSugar`
- `CoffeeWithWhippedCream`
- ... and many more combinations (exponential growth!)

This creates a **"class explosion"** problem and violates the **Open/Closed Principle**.

### The Solution
Instead, use decorators to wrap the original object and add functionality on-the-fly:
```
Original Coffee → Wrap with Milk → Wrap with Sugar → Wrap with Whipped Cream → Final Coffee
```

---

## How Does It Work?

### Core Components

1. **Component Interface**: Defines the interface that both the original object and decorators will implement
2. **Concrete Component**: The original object that has basic functionality
3. **Decorator (Abstract)**: Implements the same interface and wraps the component
4. **Concrete Decorators**: Extend the Decorator class to add specific functionality

### Step-by-Step Process

```
1. Create a Component interface (IComponent)
   ↓
2. Create a Concrete Component (ConcreteComponent)
   ↓
3. Create an Abstract Decorator (implements IComponent, wraps IComponent)
   ↓
4. Create Concrete Decorators (each adds specific functionality)
   ↓
5. Stack decorators as needed at runtime
```

### Code Flow Example

```
IComponent component = new ConcreteComponent();           // Basic object
component = new DecoratorA(component);                    // Add feature A
component = new DecoratorB(component);                    // Add feature B
component = new DecoratorC(component);                    // Add feature C
int result = component.Operation();                        // Execute with all decorations
```

---

## Key Concepts

### 1. **Composition over Inheritance**
- Decorators use composition instead of inheritance
- Objects are wrapped rather than extended through subclassing
- More flexible and avoids class explosion

### 2. **Runtime Flexibility**
- Decorators are applied dynamically at runtime
- You can choose which decorators to apply based on conditions
- Easy to combine decorators in different ways

### 3. **Single Responsibility Principle**
- Each decorator handles one specific concern
- The wrapped object doesn't need to know about decorators
- Decorators can be tested independently

### 4. **Chain of Responsibility**
- Decorators form a chain
- Each decorator passes the request through to the wrapped object
- Each can add behavior before/after delegation

---

## When to Use Decorator Pattern

### ✅ Use Decorator Pattern When:

1. **You need to add responsibilities to individual objects dynamically**
   - Not all objects of a class need the same functionality
   - Responsibilities may be added/removed at runtime

2. **Subclassing would create too many classes**
   - Many combinations of features = too many subclasses
   - You want to avoid the "class explosion" problem

3. **You want to avoid modifying existing classes**
   - Open/Closed Principle: Open for extension, closed for modification
   - Adding a decorator doesn't touch the original class

4. **Features are orthogonal (independent)**
   - Logging, validation, caching can be applied independently
   - Order may matter but features don't depend on each other

5. **You need to wrap multiple objects with the same decorator**
   - Apply the same decorator to different component implementations
   - Decorators are generic and reusable

### ❌ Don't Use Decorator Pattern When:

1. **Simple inheritance would suffice**
   - Just one or two extensions needed
   - Use-case is straightforward and unlikely to change

2. **Performance is critical**
   - Each decorator adds a layer of indirection
   - Multiple decorators = function call overhead

3. **The feature set is fixed and well-defined**
   - Features won't be added/removed dynamically
   - All combinations are known upfront

4. **Debugging is more important than flexibility**
   - Decorators make call stacks deeper and harder to debug
   - Multiple layers of wrapping complicate stack traces

---

## Real-World Examples

### Example 1: Text Formatting (Console App)
```
Original: "Hello"
With Bold Decorator: "*Hello*"
With Italic Decorator: "_*Hello*_"
With Underline Decorator: "~_*Hello*_~"
```

### Example 2: Coffee Shop (Classic)
```
Base Coffee: $5
+ Extra Shot: +$1
+ Milk: +$0.50
+ Whipped Cream: +$1
Total: $7.50
```

### Example 3: HTTP Request Processing (API)
```
HTTP Request
    ↓ [Authentication Decorator] - Validate token
    ↓ [Logging Decorator] - Log request details
    ↓ [Validation Decorator] - Validate input
    ↓ [Caching Decorator] - Check cache
    ↓ [Original Handler] - Process request
    ↓ [Performance Monitoring Decorator] - Measure time
    ↓
HTTP Response
```

### Example 4: Data Persistence
```
Save to Database
    ↓ [Encryption Decorator] - Encrypt data
    ↓ [Compression Decorator] - Compress data
    ↓ [Validation Decorator] - Validate before save
    ↓ [Logging Decorator] - Log save operation
    ↓ [Original Repository] - Save to DB
```

---

## Advantages

### 1. **Flexibility**
- Add/remove features dynamically without modifying original class
- Combine features in any order at runtime

### 2. **Single Responsibility Principle**
- Each decorator has one reason to change
- Each handles one specific concern

### 3. **Open/Closed Principle**
- Open for extension (add new decorators)
- Closed for modification (don't change existing code)

### 4. **Composition Over Inheritance**
- Avoids rigid inheritance hierarchies
- Easier to test and reason about
- More flexible feature combinations

### 5. **Reusability**
- Same decorator can be applied to different components
- Works with any implementation of the interface

### 6. **Runtime Behavior**
- Decide at runtime which decorators to apply
- Create different combinations for different scenarios

### 7. **Separation of Concerns**
- Business logic stays in the component
- Cross-cutting concerns go in decorators

---

## Disadvantages

### 1. **Increased Complexity**
- More classes and objects to manage
- Harder to understand call flow
- Can be overkill for simple cases

### 2. **Debugging Difficulty**
- Deeper call stacks with wrapped objects
- Harder to trace execution path
- Need to understand decorator chain

### 3. **Performance Overhead**
- Each decorator adds function call overhead
- Multiple decorators = multiple layers of indirection
- Not suitable for performance-critical code

### 4. **Order Dependency**
- Sometimes order of decorators matters
- Can lead to subtle bugs if order is wrong
- Need documentation of correct order

### 5. **Parameter Handling**
- Decorators must pass through constructor parameters
- Decorators must handle method parameters correctly
- Risk of parameter loss in wrapping

### 6. **Identity Issues**
- Decorated object is different from original
- Type checking (instanceof) may fail
- `GetType()` returns decorator type, not original

---

## Design Principles

### 1. **Open/Closed Principle (OCP)**
- Classes should be open for extension
- But closed for modification
- Decorators extend functionality without modifying existing code

### 2. **Single Responsibility Principle (SRP)**
- Each decorator has one reason to change
- Each handles one specific concern
- Component focuses on core functionality

### 3. **Liskov Substitution Principle (LSP)**
- Decorators can substitute for components
- Client code treats them the same way
- Interface contract must be maintained

### 4. **Dependency Inversion Principle (DIP)**
- Depend on abstractions (interfaces)
- Not on concrete implementations
- Components and decorators both implement the interface

---

## Best Practices

### 1. **Define Clear Interfaces**
```csharp
// Good: Clear contract
public interface IComponent
{
    void Operation();
    int GetValue();
}
```

### 2. **Abstract Decorator Base Class**
```csharp
// Good: Centralizes common logic
public abstract class Decorator : IComponent
{
    protected IComponent _component;
    
    public Decorator(IComponent component)
    {
        _component = component;
    }
    
    public virtual void Operation()
    {
        _component.Operation();
    }
}
```

### 3. **Concrete Decorators Add Specific Behavior**
```csharp
// Good: Each decorator has one responsibility
public class LoggingDecorator : Decorator
{
    public override void Operation()
    {
        Console.WriteLine("Before operation");
        _component.Operation();
        Console.WriteLine("After operation");
    }
}
```

### 4. **Use Factory Pattern with Decorators**
```csharp
// Good: Encapsulates decorator creation
public static IComponent CreateComponent()
{
    IComponent component = new ConcreteComponent();
    component = new LoggingDecorator(component);
    component = new CachingDecorator(component);
    return component;
}
```

### 5. **Document Decorator Order**
```csharp
// If order matters, document it!
// 1. First: Validation decorator
// 2. Second: Caching decorator
// 3. Last: Logging decorator
```

### 6. **Keep Decorators Stateless**
```csharp
// Good: Decorators don't maintain state
public class LoggingDecorator : Decorator
{
    // No instance variables (stateless)
    // Only wraps and delegates
}
```

### 7. **Use Dependency Injection**
```csharp
// Good: Makes testing easier
services.AddScoped<IComponent, ConcreteComponent>();
services.AddScoped<IComponent>(sp => 
    new LoggingDecorator(sp.GetRequiredService<IComponent>()));
```

---

## Project Structure

```
Decorator-Pattern/
│
├── DECORATOR_PATTERN_GUIDE.md          # This comprehensive guide
├── README.md                            # Project overview
│
├── DecoratorPattern.sln                # Solution file
│
├── DecoratorPattern.ConsoleApp/        # Console Application Project
│   ├── DecoratorPattern.ConsoleApp.csproj
│   ├── Program.cs
│   ├── Examples/
│   │   ├── BasicDecorator/
│   │   │   ├── IComponent.cs
│   │   │   ├── ConcreteComponent.cs
│   │   │   ├── Decorator.cs
│   │   │   ├── ConcreteDecoratorA.cs
│   │   │   ├── ConcreteDecoratorB.cs
│   │   │   └── Example.cs
│   │   ├── CoffeeShop/
│   │   │   ├── ICoffee.cs
│   │   │   ├── SimpleCoffee.cs
│   │   │   ├── CoffeeDecorator.cs
│   │   │   ├── MilkDecorator.cs
│   │   │   ├── SugarDecorator.cs
│   │   │   ├── WhippedCreamDecorator.cs
│   │   │   └── CoffeeExample.cs
│   │   └── TextFormatting/
│   │       ├── IText.cs
│   │       ├── PlainText.cs
│   │       ├── TextDecorator.cs
│   │       ├── BoldDecorator.cs
│   │       ├── ItalicDecorator.cs
│   │       └── TextFormattingExample.cs
│   └── Utils/
│       └── ConsoleHelper.cs
│
└── DecoratorPattern.API/                # ASP.NET Core API Project
    ├── DecoratorPattern.API.csproj
    ├── Program.cs
    ├── appsettings.json
    ├── Controllers/
    │   ├── ProductController.cs
    │   └── ComponentController.cs
    ├── Services/
    │   ├── IProductService.cs
    │   ├── ProductService.cs
    │   ├── Decorators/
    │   │   ├── IServiceDecorator.cs
    │   │   ├── CachingDecorator.cs
    │   │   ├── LoggingDecorator.cs
    │   │   ├── ValidationDecorator.cs
    │   │   ├── PerformanceMonitoringDecorator.cs
    │   │   └── AuthenticationDecorator.cs
    │   └── ComponentServices/
    │       ├── IComponentService.cs
    │       ├── ComponentService.cs
    │       └── Decorators/
    │           └── ComponentDecorators.cs
    ├── Models/
    │   ├── Product.cs
    │   ├── Component.cs
    │   ├── ApiResponse.cs
    │   └── PerformanceMetrics.cs
    ├── Middleware/
    │   └── ExceptionHandlingMiddleware.cs
    └── Documentation/
        └── API_EXAMPLES.md
```

---

## Decorator Pattern vs. Other Patterns

| Pattern | Purpose | Use Case |
|---------|---------|----------|
| **Decorator** | Add responsibilities dynamically | Add features at runtime |
| **Inheritance** | Extend class behavior | Permanent hierarchy |
| **Strategy** | Select algorithm at runtime | Different algorithms |
| **Proxy** | Control access to object | Security, lazy loading |
| **Adapter** | Convert interface | Incompatible interfaces |
| **Facade** | Simplify complex subsystem | Hide complexity |

---

## Summary

The **Decorator Pattern** is a powerful tool for:
- ✅ Adding functionality dynamically
- ✅ Avoiding class explosion
- ✅ Following SOLID principles
- ✅ Creating flexible, maintainable code
- ✅ Separating concerns

Use it when you need **runtime flexibility** and **orthogonal features**, but be mindful of the added complexity and potential performance impact.

---

## References

- Gang of Four Design Patterns Book
- SOLID Principles
- C# Design Patterns
- ASP.NET Core Best Practices

---

**Version**: 1.0  
**Last Updated**: May 2026
