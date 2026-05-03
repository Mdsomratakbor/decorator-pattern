namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// SugarDecorator adds sugar to the coffee.
/// </summary>
public class SugarDecorator : CoffeeDecorator
{
    private const decimal SugarPrice = 0.25m;

    /// <summary>
    /// Constructor that wraps the coffee with sugar
    /// </summary>
    /// <param name="coffee">The coffee to add sugar to</param>
    public SugarDecorator(ICoffee coffee) : base(coffee)
    {
    }

    /// <summary>
    /// Adds sugar to the description
    /// </summary>
    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Sugar";
    }

    /// <summary>
    /// Adds sugar cost to the total
    /// </summary>
    public override decimal GetCost()
    {
        return base.GetCost() + SugarPrice;
    }
}
