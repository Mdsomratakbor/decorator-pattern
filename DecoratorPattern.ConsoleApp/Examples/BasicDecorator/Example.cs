namespace DecoratorPattern.ConsoleApp.Examples.BasicDecorator;

/// <summary>
/// Example demonstrating the basic decorator pattern.
/// Shows how decorators wrap a component and add behavior dynamically.
/// </summary>
public static class BasicDecoratorExample
{
    public static void Run()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║  BASIC DECORATOR PATTERN EXAMPLE       ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        Console.WriteLine("1️⃣  SCENARIO 1: Component without decorators");
        Console.WriteLine("─────────────────────────────────────────");
        IComponent component = new ConcreteComponent();
        component.Operation();
        Console.WriteLine($"Final Value: {component.GetValue()}\n");

        Console.WriteLine("2️⃣  SCENARIO 2: Component wrapped with DecoratorA");
        Console.WriteLine("─────────────────────────────────────────");
        component = new ConcreteComponent();
        component = new ConcreteDecoratorA(component);
        component.Operation();
        Console.WriteLine($"Final Value: {component.GetValue()}\n");

        Console.WriteLine("3️⃣  SCENARIO 3: Component wrapped with DecoratorA → DecoratorB");
        Console.WriteLine("─────────────────────────────────────────");
        component = new ConcreteComponent();
        component = new ConcreteDecoratorA(component);
        component = new ConcreteDecoratorB(component);
        component.Operation();
        Console.WriteLine($"Final Value: {component.GetValue()}\n");

        Console.WriteLine("4️⃣  SCENARIO 4: Component wrapped with DecoratorB → DecoratorA (order matters!)");
        Console.WriteLine("─────────────────────────────────────────");
        component = new ConcreteComponent();
        component = new ConcreteDecoratorB(component);
        component = new ConcreteDecoratorA(component);
        component.Operation();
        Console.WriteLine($"Final Value: {component.GetValue()}\n");

        Console.WriteLine("📊 ANALYSIS:");
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("• Without decorators: 5");
        Console.WriteLine("• With DecoratorA: (5 * 2) = 10");
        Console.WriteLine("• With DecoratorA → DecoratorB: (5 * 2) + 10 = 20");
        Console.WriteLine("• With DecoratorB → DecoratorA: ((5 + 10) * 2) = 30");
        Console.WriteLine("\n✅ Order of decorators matters when they're dependent!\n");
    }
}
