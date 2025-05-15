namespace MamyCare.Contracts.MotherFeatures
{
    public class PodcastResponse
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
