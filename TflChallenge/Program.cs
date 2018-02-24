using System;
using System.Net.Http;
using TflChallenge.API;
using TflChallenge.Services;

namespace TflChallenge
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var id = args[0];

            var api = new RoadApi(client);
            var roadService = new RoadService(api);

            var response = roadService.GetRoadStatusById(id).GetAwaiter().GetResult();

            Console.WriteLine(response.RoadDisplayName);
            Console.ReadLine();
        }
    }
}
