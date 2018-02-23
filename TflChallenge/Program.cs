using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tfl.Api.Presentation.Entities;

namespace TflChallenge
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var id = args[0];

            var response = callRoadApi(id).GetAwaiter().GetResult();
            Console.WriteLine(response.DisplayName);
            Console.ReadLine();
        }

        static async Task<RoadCorridor> callRoadApi(string id)
        {
            client.BaseAddress = new Uri($"https://api.tfl.gov.uk/Road/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var app_id = "";
            var app_key = "";

            var response = await client.GetAsync($"{id}?app_id={app_id}&app_key={app_key}");

            var result = await response.Content.ReadAsStringAsync();
            var roadCorridors = JsonConvert.DeserializeObject<RoadCorridor[]>(result);
            return roadCorridors.FirstOrDefault();
        }
    }
}
