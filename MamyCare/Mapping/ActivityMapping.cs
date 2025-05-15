using MamyCare.Contracts.BabyFeature;
using MamyCare.Contracts.MotherFeatures;
using MamyCare.Contracts.User;
using Mapster;

namespace MamyCare.Mapping
{
    public class ActivityMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<Activity, ActivityResponse>
                .NewConfig()
                .Map(dest => dest.title, src => src.title)
                .Map(dest => dest.content, src => src.content)
                .Map(dest => dest.imageUrl, src => src.imageUrl);

            TypeAdapterConfig<Podcast, PodcastResponse>
                 .NewConfig()
                 .Map(dest => dest.Title, src => src.Title)
                 .Map(dest => dest.URL, src => src.URL)
                .Map(dest => dest.Duration, src => src.Duration)
                .Map(dest => dest.Type, src => src.Type);

        }
    }


}
