namespace MamyCare.Contracts.User
{
    public record ChangePasswordRequest
   (
        string OldPassword,
        string NewPassword
  );
}
