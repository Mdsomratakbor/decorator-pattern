namespace DecoratorPattern.ConsoleApp.Utils;

/// <summary>
/// Helper class for console operations
/// </summary>
public static class ConsoleHelper
{
    /// <summary>
    /// Prints a section header
    /// </summary>
    /// <param name="title">The title of the section</param>
    public static void PrintSectionHeader(string title)
    {
        Console.WriteLine("\n");
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine($"║  {title.PadRight(38)}║");
        Console.WriteLine("╚════════════════════════════════════════╝");
    }

    /// <summary>
    /// Prints a separator line
    /// </summary>
    public static void PrintSeparator()
    {
        Console.WriteLine("─────────────────────────────────────────");
    }

    /// <summary>
    /// Prints a key insight
    /// </summary>
    /// <param name="insight">The insight to print</param>
    public static void PrintInsight(string insight)
    {
        Console.WriteLine($"💡 {insight}");
    }

    /// <summary>
    /// Prints a success message
    /// </summary>
    /// <param name="message">The success message</param>
    public static void PrintSuccess(string message)
    {
        Console.WriteLine($"✅ {message}");
    }

    /// <summary>
    /// Prints an info message
    /// </summary>
    /// <param name="message">The info message</param>
    public static void PrintInfo(string message)
    {
        Console.WriteLine($"ℹ️  {message}");
    }

    /// <summary>
    /// Waits for user input to continue
    /// </summary>
    public static void WaitForInput()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
