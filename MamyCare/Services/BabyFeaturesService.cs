using MamyCare.Contracts.BabyFeature;
using MamyCare.Entities;
using Mapster;

namespace MamyCare.Services
{
    public class BabyFeaturesService: IBabyFeaturesService
    {
        private readonly ApplicationDbContext _context;
        public BabyFeaturesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ActivityResponse>> ActivitiesGetAll()
        {
            var activities = await _context.Activities.ToListAsync();
            var response =activities.Adapt<List<ActivityResponse>>();
            return response;

                }
        public async Task<ActivityResponse> ActivityGetById(int activityid)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(x=>x.id== activityid);
            var response = activity.Adapt<ActivityResponse>();
            return response;

        }
        public async Task<List<RecipeResponse>> RecipesGetAll()
        {
            var Recipes = await _context.Recipes.Include(r => r.NutritionalValues).ToListAsync();
            var response = Recipes.Adapt<List<RecipeResponse>>();
            return response;

        }
        public async Task<RecipeResponse> RecipeGetById(int Reciprid)
        {
            var recipe = await _context.Recipes
                .Include(r => r.NutritionalValues)
                .FirstOrDefaultAsync(x => x.Id == Reciprid);
            var response = recipe.Adapt<RecipeResponse>();
            return response;

        }

    }
}
