using System.ComponentModel.DataAnnotations.Schema;

namespace MamyCare.Entities
{
    public class Gallary
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? BabyId { get; set; } 

        [ForeignKey("BabyId")]
        public Baby? Baby { get; set; }
    }
}
