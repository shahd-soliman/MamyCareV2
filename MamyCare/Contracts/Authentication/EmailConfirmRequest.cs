namespace MamyCare.Contracts.Authentication
{
    public record EmailConfirmRequest(
        int UserId,
        string Code
        );


}
