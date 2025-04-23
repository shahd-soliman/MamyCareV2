using MamyCare.Contracts.Authentication;

namespace MamyCare.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> Register(RequestRegister request , CancellationToken cancellationToken);
        Task<Result<AuthResponse>> Login(string Email , string password, CancellationToken cancellationToken);
       // Task<Result> ConfirmEmail(EmailConfirmRequest request );
       // Task<Result> ResendConfirmEmail(ResendEmailConfirmRequest request);
        Task<Result> SendForgetPasswordEmail(ForgetPasswordRequest request);
         Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
        
        }
}
