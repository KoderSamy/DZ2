using System;
using System.Linq;

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

        string[] rows = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        int numColumns = 2;

        if (!string.IsNullOrEmpty(content) && content != "Расписание не найдено.")
        {
            // Расчет ширины столбцов с учетом разделителей и отступов
            int columnWidth = (width - (numColumns + 1)) / numColumns; // +1 для правого отступа


            string[] headers = { "Кабинет".PadLeft((columnWidth + "Кабинет".Length) / 2).PadRight(columnWidth),
                                 "Время работы".PadLeft((columnWidth + "Время работы".Length) / 2).PadRight(columnWidth) };

            Console.WriteLine("│" + string.Join(" │ ", headers) + "│"); // Используем " │ " как разделитель
            Console.WriteLine($"├{horizontalBorder}┤");

            string[] cells = rows[0].Split(';');
            for (int i = 0; i < cells.Length; i += numColumns)
            {
                string rowString = "│";
                for (int j = 0; j < numColumns; j++)
                {
                    string cellValue = (i + j < cells.Length ? cells[i + j] : "");
                    // Добавляем проверку на последний столбец
                    string separator = (j < numColumns -1 ) ? " │ " : ""; 
                    rowString += cellValue.PadLeft((columnWidth + cellValue.Length) / 2).PadRight(columnWidth) + separator;
                }


                Console.WriteLine(rowString + "│");


                if (i + numColumns < cells.Length)
                {
                    Console.WriteLine($"│{new string('─', width)}│");
                }
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