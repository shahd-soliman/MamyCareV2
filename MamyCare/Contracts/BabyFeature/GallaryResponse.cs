namespace MamyCare.Contracts.BabyFeature
{
    public class GallaryResponse
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
