namespace MamyCare.Entities
{
    public class Podcast
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }

        public TimeSpan Duration { get; set; }
        public bool IsArabic { get; set; } = true;
    }
}
