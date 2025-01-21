using System;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Auth.LoadDoctors();
        ScheduleManager.LoadSchedule();

        while (true) 
        {
            string currentDoctor = Auth.Login();

            if (currentDoctor != null)
            {
                Console.Clear();
                ScheduleManager.ShowSchedule(currentDoctor);
            }
            else
            {
                break; 
            }
        }
    }
}