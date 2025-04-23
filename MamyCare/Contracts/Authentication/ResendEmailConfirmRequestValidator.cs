namespace MamyCare.Contracts.Authentication
{
    public class ResendEmailConfirmRequestValidator : AbstractValidator<ResendEmailConfirmRequest>
    {
        public ResendEmailConfirmRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
