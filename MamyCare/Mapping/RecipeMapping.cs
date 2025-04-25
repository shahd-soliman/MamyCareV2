using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.Hospitals;
using MamyCare.Contracts.User;
using Mapster;

namespace MamyCare.Mapping
{

    public class RecipeMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<Recipe, RecipeResponse>
            .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.Protein, src => src.NutritionalValues!.Protein)
                .Map(dest => dest.Fiber, src => src.NutritionalValues!.Fiber)
                .Map(dest => dest.NaturalSugars, src => src.NutritionalValues!.NaturalSugars)
                .Map(dest => dest.Calories, src => src.NutritionalValues!.Calories)
                .Map(dest => dest.Carbohydrates, src => src.NutritionalValues!.Carbohydrates)
                .Map(dest => dest.Carbohydrates, src => src.NutritionalValues!.Carbohydrates);



        }
    }


}

