using System.Net.Http;
using System.Threading.Tasks;

namespace TflChallenge.API
{
    public interface IRoadApi
    {
        Task<HttpResponseMessage> GetRoadById(string id);
    }
}
