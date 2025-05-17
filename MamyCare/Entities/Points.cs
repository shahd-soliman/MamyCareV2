using System.ComponentModel.DataAnnotations.Schema;

namespace MamyCare.Entities
{
    public class Points
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int? tipId { get; set; }
        [ForeignKey("tipId")]

        public Tip ?Tip { get; set; }
    }
}
