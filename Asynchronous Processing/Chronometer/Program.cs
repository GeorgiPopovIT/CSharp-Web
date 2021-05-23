using System;
using System.Threading.Tasks;

namespace Chronometer
{
    class Program
    {
        static void Main(string[] args)
        {
            var chronometer = new Chronometer();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    Environment.Exit(0);
                }

                if (input == "start")
                {
                    chronometer.Start();
                }
                else if (input == "stop")
                {
                    chronometer.Stop();
                }
                else if (input == "lap")
                {
                    Console.WriteLine(chronometer.Lap());
                }
                else if (input == "laps")
                {
                    if (chronometer.Laps.Count == 0)
                    {
                        Console.WriteLine("no laps");
                        continue;
                    }
                    Console.WriteLine("Laps:");
                    for (int i = 0; i < chronometer.Laps.Count; i++)
                    {
                        Console.WriteLine($"{i} {chronometer.Laps[i]}");
                    }
                }
                else if (input == "reset")
                {
                    chronometer.Reset();
                }
                else if (input == "time")
                {
                    Console.WriteLine(chronometer.GetTime);
                }
            }
        }
    }
}
