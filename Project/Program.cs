using System;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Auth.LoadDoctors();
        ScheduleManager.LoadSchedule();

        string currentDoctor = Auth.Login();

        if (currentDoctor != null)
        {
            Console.Clear(); // Очищаем консоль после успешного входа
            ScheduleManager.ShowSchedule(currentDoctor);
        }
        else
        {
            Console.WriteLine("Авторизация не выполнена.");
            Console.ReadKey();
        }
    }
}