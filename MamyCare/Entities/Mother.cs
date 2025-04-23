using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MamyCare.Entities
{
    public class Mother
    {
       public int Id { get; set; }
        public string? Phone { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var parts = value.Split(' ', 2);
                    FirstName = parts[0];
                    LastName = parts.Length > 1 ? parts[1] : null;
                }
            }
        }


      
        public bool IsMarried { get; set; }
        
        public List<Baby> Babies { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; } = string.Empty;
        [ForeignKey("User")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? GovernorateId { get; set; }

        public List<FavouriteHospital>? FavouriteHospitals { get; set; }
        
    }
}
