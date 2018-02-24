using System.Threading.Tasks;
using TflChallenge.Models;

namespace TflChallenge.Services
{
    public interface IRoadService
    {
        Task<RoadStatusResponse> GetRoadStatusById(string id);
    }
}
