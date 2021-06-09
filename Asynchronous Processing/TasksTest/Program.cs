using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //int i = 5;
            //    var task = Task.Run(async () =>
            //     {
            //         HttpClient httpClient = new HttpClient();
            //         var url = $"https://softuni.bg/trainings/3353/csharp-web-basics-basics-may-2021/internal#lesson-272{i}";
            //         var httpResponse = await httpClient.GetAsync(url);
            //         var vic = await httpResponse.Content.ReadAsStringAsync();
            //         Console.WriteLine(vic);
            //     });
            ////Task.WaitAll(task);

            //var evenNumberTask = Task.Run(() =>
            //{
            //    for (int i = 0; i <= 1000; i+=2)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            //var oddNumberTask = Task.Run(() =>
            //{
            //    for (int i = 1; i <= 999; i += 2)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            //Task.WaitAll(oddNumberTask,evenNumberTask);
            //var sum = 0;
            //var task = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        sum += i;
            //        Console.Write(",");
            //        Thread.Sleep(100);
            //    }
            //});

            //while (true)
            //{
            //    string line = Console.ReadLine();
            //    if (line == "exit")
            //    {
            //        Console.WriteLine($"End sum: {sum}");
            //        return;
            //    }
            //    else if (line == "sum")
            //    {
            //        Console.WriteLine($"Current sum: {sum}");
            //    }
            //}
            //task.Wait();


            //var list = new List<Task>();
            //for (int i = 0; i < 10; i++)
            //{
            //    var task = Task.Run(() =>
            //    {
            //        Console.WriteLine(i);
            //   });
            //    list.Add(task);
            //}
            //await Task.WhenAll(list);
            //Console.WriteLine("Done");
            //Console.WriteLine("AFTER PAUSE");


            //var task = Task.Run(() => GetResponse());


            int[] arr = new int[] {1,2,3,443,12,54,23,78,6,56 ,98};
                
            var l = arr.Where(x => x % 2 == 0).AsParallel().Distinct();
            Console.WriteLine(string.Join(" ",l));


            //Console.OutputEncoding = Encoding.UTF8;
            //string url = "https://softuni.bg/courses/csharp-web-basics";
            //HttpClient httpClient = new HttpClient();
            //var response = await httpClient.GetAsync(url);
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(string.Join(Environment.NewLine,
            //  response.Headers.Select(x => x.Key + ": " + x.Value.First())));

            //var html = await httpClient.GetStringAsync(url);
            //Console.WriteLine(html);

        }
        private static async Task GetResponse()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://softuni.bg");

            Console.WriteLine(response);
        }
    }
}
