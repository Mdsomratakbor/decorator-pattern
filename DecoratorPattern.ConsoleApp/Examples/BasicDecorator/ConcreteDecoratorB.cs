namespace DecoratorPattern.ConsoleApp.Examples.BasicDecorator;

/// <summary>
/// ConcreteDecoratorB adds specific behavior to the component.
/// It adds 10 to the value.
/// </summary>
public class ConcreteDecoratorB : Decorator
{
    /// <summary>
    /// Constructor that wraps the component
    /// </summary>
    /// <param name="component">The component to be decorated</param>
    public ConcreteDecoratorB(IComponent component) : base(component)
    {
    }

    /// <summary>
    /// Adds behavior: prints decorator info and delegates to wrapped component
    /// </summary>
    public override void Operation()
    {
        Console.WriteLine("ConcreteDecoratorB: Adding behavior B (add 10)");
        base.Operation();
    }

    /// <summary>
    /// Adds behavior: adds 10 to the wrapped component's value
    /// </summary>
    public override int GetValue()
    {
        int baseValue = base.GetValue();
        int result = baseValue + 10;
        Console.WriteLine($"  DecoratorB transforms {baseValue} → {result}");
        return result;
    }
}
