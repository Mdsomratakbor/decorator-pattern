namespace DecoratorPattern.ConsoleApp.Examples.CoffeeShop;

/// <summary>
/// Example demonstrating the decorator pattern using a real-world coffee shop scenario.
/// Shows how to build different coffee combinations dynamically.
/// </summary>
public static class CoffeeShopExample
{
    public static void Run()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║  COFFEE SHOP DECORATOR EXAMPLE         ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        Console.WriteLine("☕ Building different coffee combinations...\n");

        // Scenario 1: Simple coffee
        Console.WriteLine("1️⃣  Scenario 1: Simple Coffee (No additions)");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee1 = new SimpleCoffee();
        PrintCoffeeDetails(coffee1);

        // Scenario 2: Coffee with milk
        Console.WriteLine("\n2️⃣  Scenario 2: Coffee with Milk");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee2 = new SimpleCoffee();
        coffee2 = new MilkDecorator(coffee2);
        PrintCoffeeDetails(coffee2);

        // Scenario 3: Coffee with milk and sugar
        Console.WriteLine("\n3️⃣  Scenario 3: Coffee with Milk and Sugar");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee3 = new SimpleCoffee();
        coffee3 = new MilkDecorator(coffee3);
        coffee3 = new SugarDecorator(coffee3);
        PrintCoffeeDetails(coffee3);

        // Scenario 4: Coffee with milk, sugar, and whipped cream
        Console.WriteLine("\n4️⃣  Scenario 4: Coffee with Milk, Sugar, and Whipped Cream");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee4 = new SimpleCoffee();
        coffee4 = new MilkDecorator(coffee4);
        coffee4 = new SugarDecorator(coffee4);
        coffee4 = new WhippedCreamDecorator(coffee4);
        PrintCoffeeDetails(coffee4);

        // Scenario 5: Different combination - Whipped cream only
        Console.WriteLine("\n5️⃣  Scenario 5: Coffee with only Whipped Cream");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee5 = new SimpleCoffee();
        coffee5 = new WhippedCreamDecorator(coffee5);
        PrintCoffeeDetails(coffee5);

        // Scenario 6: Sugar and whipped cream (no milk)
        Console.WriteLine("\n6️⃣  Scenario 6: Coffee with Sugar and Whipped Cream");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee6 = new SimpleCoffee();
        coffee6 = new SugarDecorator(coffee6);
        coffee6 = new WhippedCreamDecorator(coffee6);
        PrintCoffeeDetails(coffee6);

        // Scenario 7: Multiple additions with double milk
        Console.WriteLine("\n7️⃣  Scenario 7: Coffee with Double Milk, Sugar, and Whipped Cream");
        Console.WriteLine("─────────────────────────────────────────");
        ICoffee coffee7 = new SimpleCoffee();
        coffee7 = new MilkDecorator(coffee7);
        coffee7 = new MilkDecorator(coffee7); // Add milk twice
        coffee7 = new SugarDecorator(coffee7);
        coffee7 = new WhippedCreamDecorator(coffee7);
        PrintCoffeeDetails(coffee7);

        Console.WriteLine("\n📊 KEY INSIGHTS:");
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("• Each decorator adds ONE specific feature");
        Console.WriteLine("• Features are combined dynamically at runtime");
        Console.WriteLine("• No 'class explosion' problem");
        Console.WriteLine("• Easy to add new decorators (new additions) without modifying existing code");
        Console.WriteLine("• Costs are calculated compositionally (sum of all decorators)\n");
    }

    /// <summary>
    /// Helper method to print coffee details
    /// </summary>
    private static void PrintCoffeeDetails(ICoffee coffee)
    {
        Console.WriteLine($"   Description: {coffee.GetDescription()}");
        Console.WriteLine($"   Cost: ${coffee.GetCost():F2}");
    }
}
