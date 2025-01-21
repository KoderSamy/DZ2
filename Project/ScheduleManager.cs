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
                var parts = line.Split(':');
                schedule[parts[0]] = parts[1];
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