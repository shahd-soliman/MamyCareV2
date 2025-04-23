using System.ComponentModel.DataAnnotations;

namespace MamyCare.Contracts.Reminders
{
    public record ReminderResponse

     (
        int id,
        string Title,
         string Description,
         bool IsActive,
         DateTime Date

     );
}
