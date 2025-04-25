using MamyCare.Contracts.BabyFeature;
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
        }
    }
    
    
}
