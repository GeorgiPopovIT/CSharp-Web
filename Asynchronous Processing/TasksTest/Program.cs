using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TasksTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int i = 5;
                var task = Task.Run(async () =>
                 {
                     HttpClient httpClient = new HttpClient();
                     var url = $"https://softuni.bg/trainings/3353/csharp-web-basics-basics-may-2021/internal#lesson-272{i}";
                     var httpResponse = await httpClient.GetAsync(url);
                     var vic = await httpResponse.Content.ReadAsStringAsync();
                     Console.WriteLine(vic);
                 });
            //Task.WaitAll(task);

            task.Wait();

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
    }
}
