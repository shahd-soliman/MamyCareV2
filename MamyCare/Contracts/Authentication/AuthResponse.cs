namespace MamyCare.Contracts.Authentication
{
    public record AuthResponse(
    int Id,
    string Name,
    string? Email,
    string Token,
    List<Baby> Babies,
    string? ImageUrl
);
}