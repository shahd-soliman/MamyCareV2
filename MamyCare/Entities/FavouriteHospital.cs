using System.Text.Json.Serialization;

namespace MamyCare.Entities
{
    public class FavouriteHospital
    {
        public int motherId { get; set; }
        public int hospitalId { get; set; }
        public Hospital Hospital { get; set; }
        [JsonIgnore]

        public Mother Mother { get; set; }
        public bool? Isfavourite { get; set; }


    }
}
