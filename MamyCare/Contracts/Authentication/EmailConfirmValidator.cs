namespace MamyCare.Contracts.Authentication
{
    public class EmailConfirmValidator : AbstractValidator<EmailConfirmRequest>
    {
        public EmailConfirmValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
