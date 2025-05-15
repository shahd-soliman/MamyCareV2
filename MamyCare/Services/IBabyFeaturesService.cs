using MamyCare.Contracts.BabyFeature;

namespace MamyCare.Services
{
    public interface IBabyFeaturesService
    {
        Task<List<ActivityResponse>> ArabicActivitiesGetAll();
        Task<List<ActivityResponse>> EnglishActivitiesGetAll();


        Task<ActivityResponse> ActivityGetById(int activityid);
        Task<List<RecipeResponse>> RecipesGetAll();

        Task<RecipeResponse> RecipeGetById(int activityid);

    }
}
