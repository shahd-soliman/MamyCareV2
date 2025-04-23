namespace MamyCare.Contracts.User
{
    public class ChangePasswordRequestValidator:  AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
    {

        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty().NotEqual(x=>x.OldPassword);
    }
}
}
