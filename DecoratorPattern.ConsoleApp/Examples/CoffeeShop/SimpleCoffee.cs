namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// SimpleCoffee is the basic coffee without any additions.
/// </summary>
public class SimpleCoffee : ICoffee
{
    private const decimal BasePrice = 5.00m;

    public string GetDescription()
    {
        return "Simple Coffee";
    }

    public decimal GetCost()
    {
        return BasePrice;
    }
}
