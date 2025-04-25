using MamyCare.Data;
using MamyCare.Entities.MamyCare.Data;
using System.Text.Json;

namespace MamyCare.DataSeed
{
    public class ContextDataSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.NutritionalValues.Any())
            {
                var TypeNutritionalValues = File.ReadAllText("DataSeed/NutritionValues.json");
                var brands = JsonSerializer.Deserialize<List<NutritionalValue>>(TypeNutritionalValues);
                if (brands?.Count > 0)
                {
                    foreach (var item in brands)
                    {
                        context.NutritionalValues.Add(item);

                        await context.SaveChangesAsync();
                    }
                }

            }
            if (!context.Recipes.Any())
            {
                var TypeRecipes = File.ReadAllText("DataSeed/Recipes.json");
                var Types = JsonSerializer.Deserialize<List<Recipe>>(TypeRecipes);
                if (Types?.Count > 0)
                {
                    foreach (var item in Types)
                    {
                        context.Recipes.Add(item);

                        await context.SaveChangesAsync();
                    }
                }

            }
            //if (!context.Products.Any())
            //{
            //    var productsData = File.ReadAllText("../E-Commerce.EF/DataSeed/products.json");
            //    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            //    if (products?.Count > 0)
            //    {
            //        foreach (var item in products)
            //        {
            //            context.Products.Add(item);

            //            await context.SaveChangesAsync();
            //        }
            //    }

            //}
        }



    }
}


