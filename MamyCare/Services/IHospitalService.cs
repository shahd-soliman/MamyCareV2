using MamyCare.Contracts.Hospitals;
using MamyCare.Entities;

namespace MamyCare.Services
{
    public interface IHospitalService
    {
        public Task<List<GetGovernoratesResponse>> GetAllGovernorates();
        public Task<List<GetHospitalsResponse>> GetAllHospitals(int userid);
        public Task<List<GetHospitalsResponse>> GetAllFilteredHospitals(int GovernorateId , int userid);
        public Task<GetHospitalsResponse> GetById(int id, int userid);
        public Task<Result> AddToFav(int hospitalId, int userId, CancellationToken cancellationToken);
        public Task<Result<List<GetHospitalsResponse>>> GetFAv(int userId);
        public Task<Result> DeleteFav(int hospitalId, int userId, CancellationToken cancellationToken);


    }
}