using DecoratorPattern.ConsoleApp.Examples.BasicDecorator;
using DecoratorPattern.ConsoleApp.Examples.CoffeeShop;
using DecoratorPattern.ConsoleApp.Examples.TextFormatting;
using DecoratorPattern.ConsoleApp.Utils;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        PrintWelcome();

        while (true)
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║  DECORATOR PATTERN - CONSOLE APP       ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.WriteLine("Select an example to run:\n");
            Console.WriteLine("1. Basic Decorator Pattern");
            Console.WriteLine("2. Coffee Shop Example (Real-world scenario)");
            Console.WriteLine("3. Text Formatting Example");
            Console.WriteLine("4. Exit\n");

            Console.Write("Enter your choice (1-4): ");
            string? choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    BasicDecoratorExample.Run();
                    break;
                case "2":
                    CoffeeShopExample.Run();
                    break;
                case "3":
                    TextFormattingExample.Run();
                    break;
                case "4":
                    PrintGoodbye();
                    return;
                default:
                    Console.WriteLine("❌ Invalid choice. Please try again.");
                    continue;
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void PrintWelcome()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║                                        ║");
        Console.WriteLine("║  WELCOME TO DECORATOR PATTERN DEMO     ║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine("║  Learn how the Decorator Pattern       ║");
        Console.WriteLine("║  works with practical examples         ║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        Console.WriteLine("📚 What is the Decorator Pattern?");
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("The Decorator Pattern is a structural design pattern that");
        Console.WriteLine("allows you to attach additional responsibilities to an object");
        Console.WriteLine("dynamically. It provides a flexible alternative to subclassing");
        Console.WriteLine("for extending functionality.\n");

        Console.WriteLine("🎯 Key Benefits:");
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("✓ Avoid class explosion from subclassing");
        Console.WriteLine("✓ Add functionality dynamically at runtime");
        Console.WriteLine("✓ Follow Single Responsibility Principle");
        Console.WriteLine("✓ Combine features in any order\n");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    static void PrintGoodbye()
    {
        Console.Clear();
        Console.WriteLine("\n╔════════════════════════════════════════╗");
        Console.WriteLine("║                                        ║");
        Console.WriteLine("║  Thank you for learning about the      ║");
        Console.WriteLine("║  Decorator Pattern!                    ║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine("║  Check the API project for real-world  ║");
        Console.WriteLine("║  HTTP request handling examples        ║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");
    }
}
