namespace MamyCare.Contracts.Authentication
{
    public record AuthResponse(
    int Id,
    string Name,
    string? Email,
    string Token,
    List<BabyResponse> Babies,
    string? ImageUrl
);
}