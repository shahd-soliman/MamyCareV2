using System.ComponentModel.DataAnnotations.Schema;

namespace MamyCare.Entities
{
    public class TipsAndTricks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("TipId")]

        public List<Tip>? Tips { get; set; }

        public int TipId { get; set; }
        public string ImagePath { get; set; }
    }
}
