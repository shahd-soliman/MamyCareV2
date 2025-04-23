namespace MamyCare.Contracts.Reminders
{
    public class RemindResponseValidator : AbstractValidator<ReminderResponse>
    {
        public RemindResponseValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
