using MamyCare.Contracts.User;
using Mapster;

namespace MamyCare.Mapping
{
    public class BabyMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddBabyRequest, Baby>()
                .Map(dest => dest.BabyName, src => src.BabyName)
                .Map(dest => dest.BirthDate, src => src.BirthDate)
                .Map(dest => dest.gender, src => src.Gender)
               .Map(dest => dest.IsActive, src => true);


            config.NewConfig<Baby, chooseBabyResponse>()
                .Map(dest => dest.BabyName, src => src.BabyName)
                .Map(dest => dest.BirthDate, src => src.BirthDate)
                .Map(dest => dest.Gender, src => src.gender)
                .Map(dest => dest.BabyImageUrl, src => src.ProfilePicUrl);

        }
    }
}
