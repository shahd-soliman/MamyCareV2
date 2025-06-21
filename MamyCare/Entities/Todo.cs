using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MamyCare.Entities
{
    public class Todo
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public TimeOnly? Date { get; set; } 
        public Baby Baby { get; set; }
        [ForeignKey("Baby")]
        public int BabyId { get; set; }

        public bool Isdone { get; set; } = true;

    }
}
