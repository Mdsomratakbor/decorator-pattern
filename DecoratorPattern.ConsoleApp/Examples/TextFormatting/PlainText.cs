namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// PlainText is the basic text without any formatting.
/// </summary>
public class PlainText : IText
{
    private readonly string _text;

    /// <summary>
    /// Constructor that initializes the plain text
    /// </summary>
    /// <param name="text">The text to format</param>
    public PlainText(string text)
    {
        _text = text;
    }

    /// <summary>
    /// Returns the plain text without any formatting
    /// </summary>
    public string GetFormattedText()
    {
        return _text;
    }
}
