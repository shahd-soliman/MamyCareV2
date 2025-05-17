using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MamyCare.Entities
{
    public class Baby 
    {
        public int id { get; set; }
        public string BabyName { get; set; }

        public DateOnly BirthDate { get; set; }

        public string? ProfilePicUrl { get; set; }    
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                int age = today.Year - BirthDate.Year;

                if (today < BirthDate.AddYears(age))
                {
                    age--;
                }

                return age;
            }

        }

        public Gender gender { get; set; }

       public bool? IsActive { get; set; } = true;
        public int motherId { get; set; }
        [ForeignKey(nameof(motherId))]
        [JsonIgnore]
        public Mother Mother { get; set; }

        public List<Reminder>? Reminders { get; set; }
        public List<Gallary>? Gallary { get; set; }
    }
}
