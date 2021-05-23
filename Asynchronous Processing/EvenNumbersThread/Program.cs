using System;
using System.Threading;

namespace EvenNumbersThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(() => PrintEvenNumber(0, 100000000));
            thread.Start();
            thread.Join();
            Console.WriteLine("The thread finished");
            
        }

        static void PrintEvenNumber(int start, int end)
        {
            int counter = 0;
            for (int i = start; i <= end; i += 2)
            {
                counter++;
            }
            Console.WriteLine(counter);
        }
    }
}
