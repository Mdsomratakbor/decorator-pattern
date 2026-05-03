namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// ICoffee interface defines the contract for coffee and coffee decorators.
/// </summary>
public interface ICoffee
{
    /// <summary>
    /// Gets the description of the coffee
    /// </summary>
    string GetDescription();

    /// <summary>
    /// Gets the cost of the coffee
    /// </summary>
    decimal GetCost();
}
