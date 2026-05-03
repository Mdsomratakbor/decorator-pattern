namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// UnderlineDecorator underlines the text.
/// </summary>
public class UnderlineDecorator : TextDecorator
{
    /// <summary>
    /// Constructor that wraps the text with underline formatting
    /// </summary>
    /// <param name="text">The text to underline</param>
    public UnderlineDecorator(IText text) : base(text)
    {
    }

    /// <summary>
    /// Adds underline formatting to the text
    /// </summary>
    public override string GetFormattedText()
    {
        return $"_{base.GetFormattedText()}_";
    }
}
