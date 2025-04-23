using Mapster;

using MamyCare.Contracts.Hospitals;
namespace MamyCare.Mapping
{
    public class HospitalMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<GovernorateHospital, GetGovernoratesResponse>
                .NewConfig()
                .Map(dest => dest.GovernorateId, src => src.Id)
                .Map(dest => dest.GovernorateName, src => src.Name);

            TypeAdapterConfig<Hospital, GetHospitalsResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.rate, src => src.Rate)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .Map(dest => dest.isopended, src => src.Isopened)

                .Map(dest => dest.governorateName, src => src.Governorate!.Name);


            TypeAdapterConfig<FavouriteHospital, GetHospitalsResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Hospital.Id)
                .Map(dest => dest.Title, src => src.Hospital.Title)
                .Map(dest => dest.Description, src => src.Hospital.Description)
                .Map(dest => dest.rate, src => src.Hospital.Rate)
                .Map(dest => dest.ImageUrl, src => src.Hospital.ImageUrl)
                .Map(dest => dest.isopended, src => src.Hospital.Isopened)
                .Map(dest => dest.governorateName, src => src.Hospital.Governorate!.Name)
                .Map(dest => dest.IsFavourite, src => src.Isfavourite);

        }
    }
}
