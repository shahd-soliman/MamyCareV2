namespace MamyCare.Contracts.Authentication
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Code).NotEmpty();
        }

    }
}
