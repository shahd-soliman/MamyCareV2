using Mapster;
using MamyCare.Contracts.Authentication;
using MamyCare.Contracts.User;
namespace MamyCare.Mapping
{
    public class MotherMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RequestRegister, Mother>()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.IsMarried, src => src.IsMarried);



            // throw new NotImplementedException()
            config.NewConfig<RequestRegister, Mother>()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.IsMarried, src => src.IsMarried);


            TypeAdapterConfig<ApplicationUser, GetProfileResponse>
       .NewConfig()
       .Map(dest => dest.FullName, src => $"{src.Mother.FirstName} {src.Mother.LastName}".Trim())
       .Map(dest => dest.Email, src => src.Email)
       .Map(dest => dest.ImageUrl, src => src.Mother.ImageUrl); // Assuming 'src.Image' contains the image URL or path

        }
    }
}
