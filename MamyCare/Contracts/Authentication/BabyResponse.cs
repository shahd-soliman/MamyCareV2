namespace MamyCare.Contracts.Authentication
{
    public class BabyResponse
    {
        public string name { get; set; }
        public string imageurl { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender gender { get; set; }

    }
}
