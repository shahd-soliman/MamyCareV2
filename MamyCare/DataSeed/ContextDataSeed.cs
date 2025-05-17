using MamyCare.Data;
using MamyCare.Entities;
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
            if (!context.Hospitals.Any())
            {
                var HospitalsData = File.ReadAllText("DataSeed/hospitals.json");
                var Hospitals = JsonSerializer.Deserialize<List<Hospital>>(HospitalsData);
                if (Hospitals?.Count > 0)
                {
                    foreach (var item in Hospitals)
                    {
                        context.Hospitals.Add(item);

                        await context.SaveChangesAsync();
                    }
                }

            }
            if (!context.Articles.Any())
            {
                var ArticlesData = File.ReadAllText("DataSeed/English-Articles.json");
                var articles = JsonSerializer.Deserialize<List<Article>>(ArticlesData);
                if (articles?.Count > 0)
                {
                    foreach (var item in articles)
                    {
                        context.Articles.Add(item);

                        await context.SaveChangesAsync();
                    }
                }


            }
            if (!context.Activities.Any())
            {
                var ActivitiesData = File.ReadAllText("DataSeed/Activites.json");
                var activities = JsonSerializer.Deserialize<List<Activity>>(ActivitiesData);
                if (activities?.Count > 0)
                {
                    foreach (var item in activities)
                    {
                        context.Activities.Add(item);

                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Podcasts.Any())
            {
                var PodcastsData = File.ReadAllText("DataSeed/English-podcasts.json");
                var Podcasts = JsonSerializer.Deserialize<List<Podcast>>(PodcastsData);
                if (Podcasts?.Count > 0)
                {
                    foreach (var item in Podcasts)
                    {
                        context.Podcasts.Add(item);

                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Articles.Any())
            {
                var ArabicArticlesData = File.ReadAllText("DataSeed/Arabic-Articles.json");
                var Arabicarticles = JsonSerializer.Deserialize<List<Article>>(ArabicArticlesData);
                if (Arabicarticles?.Count > 0)
                {
                    foreach (var item in Arabicarticles)
                    {
                        context.Articles.Add(item);

                        await context.SaveChangesAsync();
                    }
                }


            }
            if (!context.Videos.Any())
            {
                var VideosData = File.ReadAllText("DataSeed/videos.json");
                var videos = JsonSerializer.Deserialize<List<Video>>(VideosData);
                if (videos?.Count > 0)
                {
                    foreach (var item in videos)
                    {
                        context.Videos.Add(item);

                        await context.SaveChangesAsync();
                    }
                }


            }


        }
    }
    }

    




    



