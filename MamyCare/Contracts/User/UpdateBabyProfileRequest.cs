using System;

namespace MamyCare.Contracts.User
{
    public class UpdateBabyProfileRequest
    {
        public string BabyName { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender gender { get; set; }
        public IFormFile? Image { get; set; }



    }
}
