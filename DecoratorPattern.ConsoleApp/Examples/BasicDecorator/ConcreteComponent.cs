namespace DecoratorPattern.ConsoleApp.Examples.BasicDecorator;

/// <summary>
/// ConcreteComponent is the basic object that gets decorated.
/// It provides the basic functionality that decorators will extend.
/// </summary>
public class ConcreteComponent : IComponent
{
    private readonly int _baseValue = 5;

    public void Operation()
    {
        Console.WriteLine($"ConcreteComponent: Base Operation with value {_baseValue}");
    }

    public int GetValue()
    {
        return _baseValue;
    }
}
