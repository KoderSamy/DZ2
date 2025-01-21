using System;
using System.Collections.Generic;
using System.IO;

public class ScheduleManager
{
    public static Dictionary<string, string> schedule = new Dictionary<string, string>();

    public static void LoadSchedule()
    {
        try
        {
            foreach (var line in File.ReadLines("schedule.txt"))
            {
                int colonIndex = line.IndexOf(':');

                if (colonIndex != -1)
                {
                    string doctor = line.Substring(0, colonIndex);
                    string scheduleInfo = line.Substring(colonIndex + 1);
                    schedule[doctor] = scheduleInfo;
                }
            }

            // Отладочный вывод (можно удалить после проверки)
            Console.WriteLine("Содержимое словаря schedule:");
            foreach (var kvp in schedule)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл schedule.txt не найден.");
            Environment.Exit(1);
        }
    }

    public static void ShowSchedule(string doctor)
    {
        if (schedule.TryGetValue(doctor, out var doctorSchedule))
        {
            UI.DrawWindow($"Расписание врача {doctor}", doctorSchedule);
        }
        else
        {
            UI.DrawWindow($"Расписание врача {doctor}", "Расписание не найдено.");
        }
        Console.ReadKey(true);
    }
}