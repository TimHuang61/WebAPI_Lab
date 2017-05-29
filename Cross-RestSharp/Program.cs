using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Cross_RestSharp
{
    class Program
    {
        //Step1: Nuget => Install-Package RestSharp

        private static string requestbinUrl = "https://requestb.in/158gs2d1";

        static void Main(string[] args)
        {
            GetData();
            PostData();
            JsonData();

            Console.Read();
        }

        static void GetData()
        {
            var client = new RestClient(requestbinUrl);
            var request = new RestRequest(string.Empty, Method.GET);
            var response = client.Execute(request);
            var content = response.Content;

            Console.WriteLine(content);
        }

        static void PostData()
        {
            var client = new RestClient(requestbinUrl);
            var request = new RestRequest(string.Empty, Method.POST);
            request.AddParameter("name", "value");
            request.AddQueryParameter("id", "1");
            var response = client.Execute(request);
            var content = response.Content;

            Console.WriteLine(content);
        }

        static void JsonData()
        {
            var client = new RestClient(requestbinUrl);
            var request = new RestRequest(string.Empty, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                ItemName = "BodyTest",
                IsDone = "true"
            });
            var response = client.Execute(request);
            var content = response.Content;

            Console.WriteLine(content);
        }
    }
}
