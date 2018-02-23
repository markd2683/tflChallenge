using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tfl.Api.Presentation.Entities;
using TflChallenge.API;

namespace TflChallenge
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var id = args[0];

            var roadCorridor = callRoadApi(id).GetAwaiter().GetResult();
            Console.WriteLine(roadCorridor.DisplayName);
            Console.ReadLine();
        }

        static async Task<RoadCorridor> callRoadApi(string id)
        {
            var roadApi = new RoadApi(client);

            var response = await roadApi.GetRoadById(id);

            var result = await response.Content.ReadAsStringAsync();
            var roadCorridors = JsonConvert.DeserializeObject<RoadCorridor[]>(result);
            return roadCorridors.FirstOrDefault();
        }
    }
}
