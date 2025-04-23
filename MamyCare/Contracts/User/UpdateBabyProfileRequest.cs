using System;

namespace MamyCare.Contracts.User
{
    public class UpdateBabyProfileRequest
    {
        public string BabyName { get; set; }
        public DateOnly BirthDate { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public Gender gender { get; set; }
        public IFormFile? Image { get; set; }



    }
}
