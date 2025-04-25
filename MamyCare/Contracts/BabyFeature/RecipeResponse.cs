namespace MamyCare.Contracts.BabyFeature
{
    public class RecipeResponse
    {
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public string Calories { get; set; }
        public string Carbohydrates { get; set; }
        public string Fiber { get; set; }
        public string Protein { get; set; }
        public string NaturalSugars { get; set; }
        public string ImageUrl { get; set; }
    }

}
