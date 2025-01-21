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
        int windowWidth = Console.WindowWidth - 2; // -2 для вертикальных границ
        string horizontalBorder = string.Concat(Enumerable.Repeat("─", windowWidth));
        string topBorder = $"┌{horizontalBorder}┐";
        string bottomBorder = $"└{horizontalBorder}┘";

        Console.WriteLine(topBorder);
        Console.WriteLine($"│{title.PadLeft((windowWidth + title.Length) / 2).PadRight(windowWidth)}│");
        Console.WriteLine($"├{horizontalBorder}┤");

        if (!string.IsNullOrEmpty(content) && content != "Расписание не найдено.")
        {
            string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            string[] scheduleData = content.Split(';');

            int numColumns = 3;
            int separatorWidth = (numColumns - 1) * 3; // Ширина разделителей " | "
            int availableWidth = windowWidth - separatorWidth;  // Доступная ширина для колонок
            int columnWidth = availableWidth / numColumns;

            // Округление ширины колонок для равномерного распределения
            int remainder = availableWidth % numColumns;
            int[] columnWidths = new int[numColumns];
            for (int i = 0; i < numColumns; i++)
            {
                columnWidths[i] = columnWidth + (i < remainder ? 1 : 0);
            }


            string[] headers = {
                CenterText("День недели", columnWidths[0]),
                CenterText("Кабинет", columnWidths[1]),
                CenterText("Время работы", columnWidths[2])
            };

            Console.WriteLine($"│{string.Join(" | ", headers)}│");
            Console.WriteLine($"├{horizontalBorder}┤");

            foreach (string day in days)
            {
                List<string[]> entriesForDay = scheduleData.Select(s => s.Split(','))
                                                            .Where(e => e.Length == 3 && e[0] == day)
                                                            .ToList();

                if (entriesForDay.Count > 0)
                {
                    string dayString = CenterText(day, columnWidths[0]);

                    for (int i = 0; i < entriesForDay.Count; i++)
                    {
                        string[] currentEntry = entriesForDay[i];
                        string cabinetStr = CenterText(currentEntry[1], columnWidths[1]);
                        string timeStr = CenterText(currentEntry[2], columnWidths[2]);
                        string dayOutput = (i == 0) ? dayString : new string(' ', columnWidths[0]);

                        Console.WriteLine($"│{dayOutput} | {cabinetStr} | {timeStr}│");
                    }

                    if (day != days.LastOrDefault(d => scheduleData.Any(s => s.StartsWith(d + ","))))
                    {
                        Console.WriteLine($"├{horizontalBorder}┤");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"│{content.PadLeft((windowWidth + content.Length) / 2).PadRight(windowWidth)}│");
        }

        Console.WriteLine(bottomBorder);
    }

    public static string CenterText(string text, int width)
    {
        int padding = (width - text.Length) / 2;
        return text.PadLeft(padding + text.Length).PadRight(width);
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