using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MamyCare.Entities
{
    public class Reminder
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "must have title")]

        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime Date { get; set; }= DateTime.Now;
        public Baby Baby { get; set; }
        [ForeignKey("Baby")]
        public int BabyId { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
