using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cross_HttpClient
{
    class Program
    {
        //申請一個test api
        private static string requestbinUrl = "https://requestb.in/158gs2d1";
        //HttpClient可重複使用，不需要使用using，若使用容易降低效能
        private static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            ReUseHttpClient(client);
            GetResponse(client);
            POSTData();
            HttpClientForCredential();

            Console.Read();
        }

        public static async Task ReUseHttpClient(HttpClient client)
        {
            var task1 = await client.GetStringAsync(requestbinUrl);
            var task2 = await client.GetStringAsync(requestbinUrl);

            Console.WriteLine("{0}:{1}", "ReUseHttpClient", task1);
            Console.WriteLine("{0}:{1}", "ReUseHttpClient", task2);
        }

        public static async Task GetResponse(HttpClient client)
        {
            try
            {
                //故意製造錯誤
                var response = await client.GetAsync(requestbinUrl + "1");
                //確認成功，如果沒有成功則會拋exception.
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("{0}:{1}", "GetResponse", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}:{1}", "GetResponse", e.Message);
            }
        }

        public static async Task POSTData()
        {
            HttpClient client = new HttpClient(new HttpClientHandler
            {
                UseProxy = false
            });
            var request = new HttpRequestMessage(HttpMethod.Post, requestbinUrl);
            request.Content = new StringContent("This is a test");
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            Console.WriteLine("{0}:{1}", "POSTData", await response.Content.ReadAsStringAsync());
        }

        public static async Task HttpClientForCredential()
        {
            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("tim", "1a2b3c4d"),
                PreAuthenticate = true
            };

            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = await client.GetAsync(requestbinUrl);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("{0}:{1}", "HttpClientForCredential", result);
        }
    }
}
