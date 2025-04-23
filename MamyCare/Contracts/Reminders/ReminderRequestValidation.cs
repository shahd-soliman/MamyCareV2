using MamyCare.Entities;

namespace MamyCare.Contracts.Reminders
{
    public class ReminderRequestValidation:AbstractValidator<ReminderRequest>
    {
        public ReminderRequestValidation() {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();

        }

    }
}
