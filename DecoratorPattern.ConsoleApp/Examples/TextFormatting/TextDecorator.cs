namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// Abstract base class for text decorators.
/// Each decorator wraps text and adds specific formatting.
/// </summary>
public abstract class TextDecorator : IText
{
    /// <summary>
    /// The wrapped text
    /// </summary>
    protected IText _text;

    /// <summary>
    /// Constructor that wraps the text
    /// </summary>
    /// <param name="text">The text to be decorated</param>
    protected TextDecorator(IText text)
    {
        _text = text;
    }

    /// <summary>
    /// Gets the formatted text by delegating to wrapped text
    /// and adding the decorator's formatting
    /// </summary>
    public virtual string GetFormattedText()
    {
        return _text.GetFormattedText();
    }
}
