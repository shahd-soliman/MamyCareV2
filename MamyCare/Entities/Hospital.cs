using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MamyCare.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       
        public bool Isopened { get; set; }
        public string? Phone { get; set; }
        public string ImageUrl { get; set; }
        public int Rate { get; set; }

        [JsonIgnore]

        public GovernorateHospital? Governorate { get; set; }
        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        [JsonIgnore]
       public List<FavouriteHospital>? FavouriteHospitals { get; set; }

       

    }
}
