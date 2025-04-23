namespace MamyCare.Authentication
{
    public interface IJwtProvider
    {
        (string Token , int Expiredate) GenerateJwtToken(ApplicationUser user);
    }
}
