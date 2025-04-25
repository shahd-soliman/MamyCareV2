namespace MamyCare.Entities
{
    namespace MamyCare.Data
    {
        public class NutritionalValue
        {
            public int Id { get; set; }
            public string Calories { get; set; }
            public string Carbohydrates { get; set; }
            public string Fiber { get; set; }
            public string Protein { get; set; }
            public string NaturalSugars { get; set; }

            public int? RecipeId { get; set; }
        }
    }
}
