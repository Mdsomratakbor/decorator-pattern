namespace DecoratorPattern.ConsoleApp.Examples.BasicDecorator;

/// <summary>
/// Abstract Decorator provides the base structure for all concrete decorators.
/// It implements IComponent and wraps another IComponent instance.
/// </summary>
public abstract class Decorator : IComponent
{
    /// <summary>
    /// Holds reference to the wrapped component
    /// </summary>
    protected IComponent _wrappedComponent;

    /// <summary>
    /// Constructor that initializes the decorator with a component
    /// </summary>
    /// <param name="component">The component to be wrapped</param>
    protected Decorator(IComponent component)
    {
        _wrappedComponent = component;
    }

    /// <summary>
    /// Default implementation delegates to the wrapped component
    /// Concrete decorators override this to add behavior
    /// </summary>
    public virtual void Operation()
    {
        _wrappedComponent.Operation();
    }

    /// <summary>
    /// Default implementation delegates to the wrapped component
    /// Concrete decorators override this to add behavior
    /// </summary>
    public virtual int GetValue()
    {
        return _wrappedComponent.GetValue();
    }
}
