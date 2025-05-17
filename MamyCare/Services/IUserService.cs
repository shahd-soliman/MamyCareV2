using MamyCare.Contracts.User;

namespace MamyCare.Services
{
    public interface IUserService
    {
        public Task<Result<GetProfileResponse>> GetMotherProfile(int userid);
        public Task<Result> UpdateProfile(int userid ,UpdateProfileRequest request);
        Task<Result> ChangePassword(int userid, ChangePasswordRequest request);
        public Task<Result> AddBaby(int UserId, AddBabyRequest request, CancellationToken cancellationToken);
        public Task<Result> UpdateBaby(int UserId, UpdateBabyProfileRequest request, CancellationToken cancellationToken);
        public Task<Result<chooseBabyResponse>> ChooseBaby(int UserId, int BabyId);
        public Task<Result<GetBabyProfileResponse>> GetBabyProfile(int userid);

    }
}
