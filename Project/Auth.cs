using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Auth
{
    public static Dictionary<string, string> doctors = new Dictionary<string, string>();

    public static void LoadDoctors()
    {
        try
        {
            foreach (var line in File.ReadLines("doctors.txt"))
            {
                var parts = line.Split(':');
                if (parts.Length == 2)
                {
                    doctors[parts[0]] = parts[1];
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл doctors.txt не найден.");
            Environment.Exit(1);
        }
    }

    public static string Login()
    {
        string login = "";
        string password = "";
        int selectedField = 0;
        bool loggedIn = false;

        while (!loggedIn)
        {
            UI.DrawLoginScreen(login, password, selectedField);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Tab)
            {
                selectedField = (selectedField + 1) % 2;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (doctors.TryGetValue(login, out var correctPassword) && correctPassword == password)
                {
                    loggedIn = true;
                    return login;
                }
                else
                {
                    UI.DrawError("Неверный логин или пароль.");
                    login = "";
                    password = "";
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return null;
            }
            else
            {
                if (selectedField == 0)
                {
                    if (key.Key == ConsoleKey.Backspace && login.Length > 0)
                        login = login.Remove(login.Length - 1);
                    else if (login.Length < 20)
                        login += key.KeyChar;
                }
                else // selectedField == 1
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        password = password.Remove(password.Length - 1);
                    else if (password.Length < 20)
                        password += key.KeyChar;
                }
            }
        }
        return null; // Этот return не достижим
    }
}