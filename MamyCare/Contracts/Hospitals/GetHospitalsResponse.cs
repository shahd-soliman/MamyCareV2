namespace MamyCare.Contracts.Hospitals
{
    public class GetHospitalsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int rate { get; set; }
        public bool IsFavourite { get; set; }=false;
        public string ImageUrl { get; set; }
        public string GovernoorateName { get; set; }
        public bool isopended { get; set; }
        public string governorateName { get; set; }
    }
}
