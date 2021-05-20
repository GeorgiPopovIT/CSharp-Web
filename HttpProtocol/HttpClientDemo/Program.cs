using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string NewLine = "\r\n";
            TcpListener tcpListener
                = new TcpListener(IPAddress.Loopback, 12345);
            tcpListener.Start();
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    byte[] buffer = new byte[100000];
                    var length = stream.Read(buffer, 0, buffer.Length);

                    string requestString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Console.WriteLine(requestString);

                    string html = $"<h1>Hello from NikiServer {DateTime.Now}</h1>" +
                        $"<form action=/tweet method=post><input name=username /><input name=password />" +
                        $"<input type=submit /></form>";

                    string response = "HTTP/1.1 200 OK" + NewLine +
                       "Server: NikiServer 2020" + NewLine +
                       // "Location: https://www.google.com" + NewLine +
                       "Content-Type: image/gif; charset=utf-8" + NewLine +
                       //"Content-Disposition: attachment; filename=niki.txt" + NewLine +
                       "Content-Lenght: " + html.Length + NewLine +
                       NewLine +
                       html + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes);

                    Console.WriteLine(new string('=', 70));
                }
            }
        }
        public async Task ReadData()
        {
            Console.OutputEncoding = Encoding.UTF8;
            HttpClient httpClient = new HttpClient();
            string url = "https://softuni.bg/trainings/3164/csharp-web-basics-september-2020/internal#lesson-18198";
            var response = await httpClient.GetAsync(url);

            Console.WriteLine(response.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine,response.Headers.Select(x => x.Key + ":" + x.Value)));

            //var html = await httpClient.GetStringAsync(url);
            // Console.WriteLine(html);
        }
    }
}
