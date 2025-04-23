namespace MamyCare.Contracts.User
{
    public class AddBabyRequest
    {
        public string BabyName { get; set; }
        public DateOnly BirthDate { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? BabyImage { get; set; }
    }
}