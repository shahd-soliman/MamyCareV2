using MamyCare.Contracts.BabyFeature;
using MamyCare.Entities;
using Mapster;
using Microsoft.Extensions.Options;

namespace MamyCare.Services
{
    public class BabyFeaturesService(IOptions<ServerSettings>options , ApplicationDbContext context) : IBabyFeaturesService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly string _baseUrl = options.Value.BaseUrl;


        public async Task<List<ActivityResponse>> ActivitiesGetAll()
        {
            var activities = await _context.Activities.ToListAsync();
            var response = activities.Adapt<List<ActivityResponse>>();

            foreach (var activity in response)
            {
                activity.imageUrl = $"{_baseUrl}{activity.imageUrl}";
            }

            return response;
        }

        public async Task<ActivityResponse> ActivityGetById(int activityid)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(x=>x.id== activityid);
            var response = activity.Adapt<ActivityResponse>();
            response.imageUrl = $"{_baseUrl}{response.imageUrl}";
            return response;

        }
        public async Task<List<RecipeResponse>> RecipesGetAll()
        {
            var Recipes = await _context.Recipes.Include(r => r.NutritionalValues).ToListAsync();
            var response = Recipes.Adapt<List<RecipeResponse>>();
            foreach (var recipe in response)
            {
                recipe.ImageUrl = $"{_baseUrl}{recipe.ImageUrl}";
            }

            return response;

        }
        public async Task<RecipeResponse> RecipeGetById(int Reciprid)
        {
            var recipe = await _context.Recipes
                .Include(r => r.NutritionalValues)
                .FirstOrDefaultAsync(x => x.Id == Reciprid);
            var response = recipe.Adapt<RecipeResponse>();
            response.ImageUrl = $"{_baseUrl}{response.ImageUrl}";
            return response;

        }

    }
}
