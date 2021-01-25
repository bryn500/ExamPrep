using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Network
{
    public class Program
    {
        private const string url = "https://raw.githubusercontent.com/bryn500/.NetCoreTemplate/master/appsettings.json";
        const string useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36";
        static async Task Main(string[] args)
        {
            // Low level HttpWebRequest
            WebRequest request = WebRequest.Create(url);
            request.Headers.Add("user-agent", useragent);

            // Get the response.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        Console.WriteLine(response.StatusCode);
                        string s = reader.ReadToEnd();
                        Console.WriteLine(s);
                    }
                }
            }

            // high-level abstraction on top of HttpWebRequest
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", useragent);

                using (Stream data = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(data))
                    {
                        Console.WriteLine("nowhere to see status code unless there's an exception ?");
                        string s = reader.ReadToEnd();
                        Console.WriteLine(s);
                    }
                }
            }

            // more modern, no need for stream handling, easier async api
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", useragent);
                var httpResponse = await httpClient.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine(httpResponse.StatusCode);
                    var s = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(s);
                }
            }
        }
    }
}
