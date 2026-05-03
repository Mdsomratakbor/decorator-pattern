namespace DecoratorPattern.ConsoleApp.Examples.BasicDecorator;

/// <summary>
/// ConcreteDecoratorA adds specific behavior to the component.
/// It multiplies the value by 2.
/// </summary>
public class ConcreteDecoratorA : Decorator
{
    /// <summary>
    /// Constructor that wraps the component
    /// </summary>
    /// <param name="component">The component to be decorated</param>
    public ConcreteDecoratorA(IComponent component) : base(component)
    {
    }

    /// <summary>
    /// Adds behavior: prints decorator info and delegates to wrapped component
    /// </summary>
    public override void Operation()
    {
        Console.WriteLine("ConcreteDecoratorA: Adding behavior A (multiply by 2)");
        base.Operation();
    }

    /// <summary>
    /// Adds behavior: multiplies the wrapped component's value by 2
    /// </summary>
    public override int GetValue()
    {
        int baseValue = base.GetValue();
        int result = baseValue * 2;
        Console.WriteLine($"  DecoratorA transforms {baseValue} → {result}");
        return result;
    }
}
