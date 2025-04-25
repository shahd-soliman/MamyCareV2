using MamyCare.Entities.MamyCare.Data;

namespace MamyCare.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }

        public string ImageUrl { get; set; }
        public int? NutritionalValuesId { get; set; }
        public NutritionalValue? NutritionalValues { get; set; }
    }
}
