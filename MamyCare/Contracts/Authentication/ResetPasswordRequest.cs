namespace MamyCare.Contracts.Authentication
{
    public record ResetPasswordRequest
    (
         string Email ,
     string NewPassword ,
     string Code 
    );
}
