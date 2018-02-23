using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TflChallenge.API
{
    public class RoadApi : IRoadApi
    {
        private readonly HttpClient client;

        public RoadApi(HttpClient client)
        {
            this.client = client;
            configureClient();
        }

        public async Task<HttpResponseMessage> GetRoadById(string id)
        {
            var app_id = ConfigurationManager.AppSettings["app_id"];
            var app_key = ConfigurationManager.AppSettings["app_key"];

            return await client.GetAsync($"{id}?app_id={app_id}&app_key={app_key}");
        }

        private void configureClient()
        {
            client.BaseAddress = new Uri($"{ConfigurationManager.AppSettings["ApiBaseUrl"]}Road/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
