using System;
using System.Linq;
using System.Text.RegularExpressions; // Добавьте using

public class UI
{
    public static void DrawLoginScreen(string login, string password, int selectedField)
    {
        Console.Clear();
        int width = Console.WindowWidth - 4;
        string horizontalBorder = string.Concat(Enumerable.Repeat("─", width));
        string topBorder = $"┌{horizontalBorder}┐";
        string bottomBorder = $"└{horizontalBorder}┘";

        Console.WriteLine(topBorder);
        Console.WriteLine($"│{"Авторизация".PadLeft((width + "Авторизация".Length) / 2).PadRight(width)}│");
        Console.WriteLine($"├{horizontalBorder}┤");
        Console.WriteLine($"│{"Логин:".PadRight(10)}{new string('_', 20).PadRight(width - 10)}│");
        Console.WriteLine($"│{"Пароль:".PadRight(10)}{new string('_', 20).PadRight(width - 10)}│");
        Console.WriteLine(bottomBorder);

        Console.SetCursorPosition(11, 3);
        Console.Write(login);
        Console.SetCursorPosition(11, 4);
        Console.Write(password);

        if (selectedField == 0)
        {
            Console.SetCursorPosition(Math.Min(login.Length + 11, 30), 3);
        }
        else
        {
            Console.SetCursorPosition(Math.Min(password.Length + 11, 30), 4);
        }
    }

    public static void DrawWindow(string title, string content, int selectedItem = 0)
    {
        int width = Console.WindowWidth - 4;
        int height = 15;
        string horizontalBorder = string.Concat(Enumerable.Repeat("─", width));
        string topBorder = $"┌{horizontalBorder}┐";
        string bottomBorder = $"└{horizontalBorder}┘";

        Console.WriteLine(topBorder);
        Console.WriteLine($"│{title.PadLeft((width + title.Length) / 2).PadRight(width)}│");
        Console.WriteLine($"├{horizontalBorder}┤");

        // Используем Regex для разделения строк:
        string[] lines = Regex.Split(content, "\r\n|\r|\n"); 

        for (int i = 0; i < height - 4; i++)
        {
            if (i < lines.Length)
            {
                Console.WriteLine($"│{lines[i].PadRight(width)}│");
            }
            else
            {
                Console.WriteLine($"│{" ".PadRight(width)}│");
            }
        }

        Console.WriteLine(bottomBorder);
    }

    public static void DrawError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(0, 6);
        Console.WriteLine(message.PadLeft((Console.WindowWidth - 4 + message.Length) / 2).PadRight(Console.WindowWidth - 4));
        Console.ResetColor();
        Console.ReadKey(true);
    }
}