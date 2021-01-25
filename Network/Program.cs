using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Network
{
    public class Program
    {
        const string url = "https://google.co.uk";
        const string useragent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
        static async Task Main(string[] args)
        {
            // Low level HttpWebRequest
            WebRequest request = WebRequest.Create(url);
            request.Headers.Add("user-agent", useragent);
            // If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //response.StatusDescription

                // Get the stream containing content returned by the server.
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
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
                        string s = reader.ReadToEnd();
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
                    var result = await httpResponse.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
