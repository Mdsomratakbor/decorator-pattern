namespace DecoratorPattern.ConsoleApp.Examples.BasicDecorator;

/// <summary>
/// IComponent interface defines the contract that both the component
/// and decorators must implement.
/// </summary>
public interface IComponent
{
    /// <summary>
    /// Performs the basic operation
    /// </summary>
    void Operation();

    /// <summary>
    /// Returns the value after all decorations are applied
    /// </summary>
    int GetValue();
}
