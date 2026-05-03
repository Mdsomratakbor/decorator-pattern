namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// Abstract base class for coffee decorators.
/// Each decorator wraps a coffee and adds a specific component.
/// </summary>
public abstract class CoffeeDecorator : ICoffee
{
    /// <summary>
    /// The wrapped coffee
    /// </summary>
    protected ICoffee _coffee;

    /// <summary>
    /// Constructor that wraps the coffee
    /// </summary>
    /// <param name="coffee">The coffee to be decorated</param>
    protected CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    /// <summary>
    /// Gets the description by delegating to wrapped coffee
    /// and adding the decorator's description
    /// </summary>
    public virtual string GetDescription()
    {
        return _coffee.GetDescription();
    }

    /// <summary>
    /// Gets the cost by delegating to wrapped coffee
    /// and adding the decorator's cost
    /// </summary>
    public virtual decimal GetCost()
    {
        return _coffee.GetCost();
    }
}
