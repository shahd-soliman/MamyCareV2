namespace MamyCare.Contracts.User
{
    public class GetBabyProfileResponse
    {
        public  string name;
        public string imageurl;
        public DateOnly BirthDate { get; set; }
        public string gender;
    }
}
