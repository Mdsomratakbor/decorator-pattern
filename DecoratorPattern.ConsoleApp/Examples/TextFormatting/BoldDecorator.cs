namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// BoldDecorator makes the text bold.
/// </summary>
public class BoldDecorator : TextDecorator
{
    /// <summary>
    /// Constructor that wraps the text with bold formatting
    /// </summary>
    /// <param name="text">The text to make bold</param>
    public BoldDecorator(IText text) : base(text)
    {
    }

    /// <summary>
    /// Adds bold formatting to the text
    /// </summary>
    public override string GetFormattedText()
    {
        return $"**{base.GetFormattedText()}**";
    }
}
