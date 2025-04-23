using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MamyCare.Contracts.Reminders
{
    public record ReminderRequest

    (
            [Required(ErrorMessage = "Must have title")]

        string Title,
        string Description,

        DateTime Date

    );
        
}
