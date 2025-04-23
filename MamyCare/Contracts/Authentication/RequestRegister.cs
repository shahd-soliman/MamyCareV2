using MamyCare.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MamyCare.Contracts.Authentication
{
    public class RequestRegister
    {
        // ApplicationUser details

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        // Mother details

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsMarried { get; set; }
        public IFormFile? MotherImage { get; set; }
       
        // Baby details
        [Required]
        public string babyName { get; set; }
        public DateOnly BirthDate { get; set; }
       // public float ?Height { get; set; }
       // public float ?weight { get; set; }
        public Gender gender { get; set; }
        public IFormFile? BabyImage { get; set; }




    }

}
