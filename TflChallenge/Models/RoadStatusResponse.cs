namespace TflChallenge.Models
{
    public class RoadStatusResponse
    {
        public string RoadId { get; set; }

        public string RoadDisplayName { get; set; }

        public RoadStatus Status { get; set; }

        public bool Success { get; set; }

        public string FriendlyMessage { get; set; }

        public string Error { get; set; }
    }
}
