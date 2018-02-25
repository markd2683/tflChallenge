using System;
using System.Net.Http;
using System.Threading.Tasks;
using TflChallenge.API;

namespace TflChallenge.Tests.Mocks
{
    public class RoadApiStub : IRoadApi
    {
        private readonly Exception exceptionToThrow;
        private readonly HttpResponseMessage responseMessageToReturn;

        public RoadApiStub(Exception exceptionToThrow)
        {
            this.exceptionToThrow = exceptionToThrow;
        }

        public RoadApiStub(HttpResponseMessage responseMessageToReturn)
        {
            this.responseMessageToReturn = responseMessageToReturn;
        }

        public async Task<HttpResponseMessage> GetRoadById(string id)
        {
            if (exceptionToThrow != null)
            {
                throw exceptionToThrow;
            }

            var responseTask = new Task<HttpResponseMessage>(() => responseMessageToReturn);
            responseTask.Start();
            return await responseTask;
        }
    }
}
