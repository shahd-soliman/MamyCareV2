namespace MamyCare.Contracts.User
{
    public record UpdateProfileRequest(
        string FullName,
        string phone,
        IFormFile? Image
        );
}
