using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;
using MamyCare.Contracts.User;
using Mapster;

namespace MamyCare.Mapping
{
    public class BabyFeaturesMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<Activity, ActivityResponse>
                .NewConfig()
                .Map(dest => dest.title, src => src.title)
                .Map(dest => dest.content, src => src.content)
                .Map(dest => dest.imageUrl, src => src.imageUrl);


            TypeAdapterConfig<Gallary, GallaryResponse>
                 .NewConfig()
                 .Map(dest => dest.Id, src => src.Id)
                 .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        }
    }


}
