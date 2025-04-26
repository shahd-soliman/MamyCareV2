namespace MamyCare.Contracts.MotherFeatures
{
    public class ArticleResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }
    }
}
