namespace MamyCare.Contracts.User
{
    public class chooseBabyResponse
    {
        public string BabyName { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string BabyImageUrl { get; set; }
    }
}