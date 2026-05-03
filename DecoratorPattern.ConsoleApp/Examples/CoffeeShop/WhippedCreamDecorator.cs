namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// WhippedCreamDecorator adds whipped cream to the coffee.
/// </summary>
public class WhippedCreamDecorator : CoffeeDecorator
{
    private const decimal WhippedCreamPrice = 1.00m;

    /// <summary>
    /// Constructor that wraps the coffee with whipped cream
    /// </summary>
    /// <param name="coffee">The coffee to add whipped cream to</param>
    public WhippedCreamDecorator(ICoffee coffee) : base(coffee)
    {
    }

    /// <summary>
    /// Adds whipped cream to the description
    /// </summary>
    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Whipped Cream";
    }

    /// <summary>
    /// Adds whipped cream cost to the total
    /// </summary>
    public override decimal GetCost()
    {
        return base.GetCost() + WhippedCreamPrice;
    }
}
