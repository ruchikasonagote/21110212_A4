using System;
using System.Timers;

public class AlarmClock
{
    public delegate void AlarmEventHandler();
    public event AlarmEventHandler? raiseAlarm;

    private DateTime targetTime;

    public void SetTargetTime(string timeStr)
    {
        if (DateTime.TryParse(timeStr, out DateTime parsedTime))
        {
            DateTime now = DateTime.Now;
            targetTime = new DateTime(
                now.Year, now.Month, now.Day,
                parsedTime.Hour, parsedTime.Minute, parsedTime.Second
            );

            if (targetTime <= now)
            {
                targetTime = targetTime.AddDays(1);
                Console.WriteLine($"Time already passed today. Alarm set for tomorrow at {targetTime.ToLongTimeString()}");
            }
            else
            {
                Console.WriteLine($"Alarm set for {targetTime.ToLongTimeString()}");
            }
        }
        else
        {
            Console.WriteLine("Invalid time format. Use HH:MM:SS");
            Environment.Exit(0);
        }
    }


    public void Start()
    {
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        timer.Elapsed += CheckTime;
        timer.Start();
    }

    private void CheckTime(object? source, ElapsedEventArgs e)
    {
        if (DateTime.Now.ToLongTimeString() == targetTime.ToLongTimeString())
        {
            raiseAlarm?.Invoke(); // Raise event
        }
    }

    public void Ring_alarm()
    {
        Console.WriteLine("Alarm ringing! Wake up!");
        Environment.Exit(0);
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter time in HH:MM:SS format: ");
        string? input = Console.ReadLine();

        if (input == null)
        {
            Console.WriteLine("No input received. Exiting.");
            return;
        }

        AlarmClock alarm = new AlarmClock();
        alarm.raiseAlarm += alarm.Ring_alarm;
        alarm.SetTargetTime(input);
        alarm.Start();

        Console.ReadLine();
    }

}