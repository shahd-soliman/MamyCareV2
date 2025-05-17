namespace MamyCare.Contracts.BabyFeature
{
    public class GallaryResponse
    {
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
