namespace MamyCare.Contracts.Authentication
{
    public class ForegetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
    {
        public ForegetPasswordRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
