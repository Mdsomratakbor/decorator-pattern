namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// MilkDecorator adds milk to the coffee.
/// </summary>
public class MilkDecorator : CoffeeDecorator
{
    private const decimal MilkPrice = 0.50m;

    /// <summary>
    /// Constructor that wraps the coffee with milk
    /// </summary>
    /// <param name="coffee">The coffee to add milk to</param>
    public MilkDecorator(ICoffee coffee) : base(coffee)
    {
    }

    /// <summary>
    /// Adds milk to the description
    /// </summary>
    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Milk";
    }

    /// <summary>
    /// Adds milk cost to the total
    /// </summary>
    public override decimal GetCost()
    {
        return base.GetCost() + MilkPrice;
    }
}
