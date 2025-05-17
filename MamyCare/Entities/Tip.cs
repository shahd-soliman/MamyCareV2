using System.ComponentModel.DataAnnotations.Schema;

namespace MamyCare.Entities
{
    public class Tip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("pointid")]
        public List<Points>? Points {get; set; }
   
        public int ?pointid { get; set; }

        [ForeignKey("TipsTricksId")]
        public int? TipsTricksId { get; set; }   
        public TipsAndTricks? TipsAndTricks { get; set; }
    }
}
