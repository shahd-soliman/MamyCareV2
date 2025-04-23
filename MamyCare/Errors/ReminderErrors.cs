namespace MamyCare.Errors
{
    public static class ReminderErrors
    {
        public static readonly Error InvalidReminder =
 new("", "Baby reminder  not found.", StatusCodes.Status400BadRequest);
    
     public static readonly Error NUllReminders =
 new("", "There is no reminders .", StatusCodes.Status400BadRequest);
    }

}
