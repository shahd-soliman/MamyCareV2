using MamyCare.Contracts.MotherFeatures;
using Mapster;

namespace MamyCare.Mapping
{
    public class MotherFeatureMapping: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Article, ArticleResponse>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Views, src => src.Views)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl);
        }
    }
    
}
