namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// IText interface defines the contract for text and text decorators.
/// </summary>
public interface IText
{
    /// <summary>
    /// Gets the formatted text
    /// </summary>
    string GetFormattedText();
}
