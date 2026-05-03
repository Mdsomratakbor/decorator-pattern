namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// ItalicDecorator makes the text italic.
/// </summary>
public class ItalicDecorator : TextDecorator
{
    /// <summary>
    /// Constructor that wraps the text with italic formatting
    /// </summary>
    /// <param name="text">The text to make italic</param>
    public ItalicDecorator(IText text) : base(text)
    {
    }

    /// <summary>
    /// Adds italic formatting to the text
    /// </summary>
    public override string GetFormattedText()
    {
        return $"_{base.GetFormattedText()}_";
    }
}
