using MamyCare.Contracts.BabyFeature;

namespace MamyCare.Services
{
    public interface IBabyFeaturesService
    {
        Task<List<ActivityResponse>> ActivitiesGetAll();

        Task<ActivityResponse> ActivityGetById(int activityid);
        Task<List<RecipeResponse>> RecipesGetAll();

        Task<RecipeResponse> RecipeGetById(int activityid);

    }
}
