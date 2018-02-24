using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tfl.Api.Presentation.Entities;
using TflChallenge.API;
using TflChallenge.Models;

namespace TflChallenge.Services
{
    public class RoadService : IRoadService
    {
        private readonly IRoadApi roadApi;

        public RoadService(IRoadApi roadApi)
        {
            this.roadApi = roadApi;
        }

        public async Task<RoadStatusResponse> GetRoadStatusById(string id)
        {
            var result = new RoadStatusResponse()
            {
                RoadId = id.ToLower(),
                Success = false
            };

            try
            {
                var response = await roadApi.GetRoadById(id);

                if (response.IsSuccessStatusCode)
                {
                    var allRoadCorridors = await deserializeContent<RoadCorridor[]>(response.Content);
                    var roadCorridor = allRoadCorridors.FirstOrDefault();

                    result.RoadDisplayName = roadCorridor.DisplayName;
                    result.Status = new RoadStatus()
                    {
                        StatusSeverity = roadCorridor.StatusSeverity,
                        StatusSeverityDescription = roadCorridor.StatusSeverityDescription
                    };
                    result.Success = true;

                    return result;
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    var error = await deserializeContent<ApiError>(response.Content);
                    result.Error = error.ExceptionType;
                    result.FriendlyMessage = error.Message;
                    result.Success = true;

                    return result;
                }

                //Any other http responses are treated as unsuccessful
                result.FriendlyMessage = $"Due to an unexpected error it has not been possible to retrieve the status of road with ID: {id}";
                result.Error = response.ReasonPhrase;
                return result;
            }
            catch (Exception ex)
            {
                result.FriendlyMessage = $"Due to an unexpected error it has not been possible to retrieve the status of road with ID: {id}";
                result.Error = ex.Message;
                return result;
            }
        }

        private async Task<T> deserializeContent<T>(HttpContent content)
        {
            var result = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
