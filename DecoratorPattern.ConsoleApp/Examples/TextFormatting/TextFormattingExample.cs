namespace DecoratorPattern.ConsoleApp.Examples.TextFormatting;

/// <summary>
/// Example demonstrating the decorator pattern using text formatting.
/// Shows how to apply multiple formatting decorators to text.
/// </summary>
public static class TextFormattingExample
{
    public static void Run()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║  TEXT FORMATTING DECORATOR EXAMPLE     ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        Console.WriteLine("📝 Applying different text formatting combinations...\n");

        // Scenario 1: Plain text
        Console.WriteLine("1️⃣  Scenario 1: Plain Text");
        Console.WriteLine("─────────────────────────────────────────");
        IText text1 = new PlainText("Hello World");
        Console.WriteLine(text1.GetFormattedText());

        // Scenario 2: Bold text
        Console.WriteLine("\n2️⃣  Scenario 2: Bold Text");
        Console.WriteLine("─────────────────────────────────────────");
        IText text2 = new PlainText("Hello World");
        text2 = new BoldDecorator(text2);
        Console.WriteLine(text2.GetFormattedText());

        // Scenario 3: Bold and Italic
        Console.WriteLine("\n3️⃣  Scenario 3: Bold and Italic");
        Console.WriteLine("─────────────────────────────────────────");
        IText text3 = new PlainText("Hello World");
        text3 = new BoldDecorator(text3);
        text3 = new ItalicDecorator(text3);
        Console.WriteLine(text3.GetFormattedText());

        // Scenario 4: Bold, Italic, and Underline
        Console.WriteLine("\n4️⃣  Scenario 4: Bold, Italic, and Underline");
        Console.WriteLine("─────────────────────────────────────────");
        IText text4 = new PlainText("Hello World");
        text4 = new BoldDecorator(text4);
        text4 = new ItalicDecorator(text4);
        text4 = new UnderlineDecorator(text4);
        Console.WriteLine(text4.GetFormattedText());

        // Scenario 5: Different order - Italic then Bold
        Console.WriteLine("\n5️⃣  Scenario 5: Italic then Bold (order matters for appearance!)");
        Console.WriteLine("─────────────────────────────────────────");
        IText text5 = new PlainText("Hello World");
        text5 = new ItalicDecorator(text5);
        text5 = new BoldDecorator(text5);
        Console.WriteLine(text5.GetFormattedText());

        // Scenario 6: Underline and Bold
        Console.WriteLine("\n6️⃣  Scenario 6: Underline and Bold");
        Console.WriteLine("─────────────────────────────────────────");
        IText text6 = new PlainText("Important Notice");
        text6 = new UnderlineDecorator(text6);
        text6 = new BoldDecorator(text6);
        Console.WriteLine(text6.GetFormattedText());

        // Scenario 7: Just Underline
        Console.WriteLine("\n7️⃣  Scenario 7: Just Underline");
        Console.WriteLine("─────────────────────────────────────────");
        IText text7 = new PlainText("Underlined Text");
        text7 = new UnderlineDecorator(text7);
        Console.WriteLine(text7.GetFormattedText());

        Console.WriteLine("\n📊 KEY INSIGHTS:");
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("• Each decorator adds ONE specific formatting");
        Console.WriteLine("• Multiple decorators can be stacked for combined formatting");
        Console.WriteLine("• Order can affect the visual appearance");
        Console.WriteLine("• No need for classes like BoldItalic, ItalicBoldUnderline, etc.");
        Console.WriteLine("• Easy to add new formatting options without modifying existing code\n");
    }
}
