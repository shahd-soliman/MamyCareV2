namespace MamyCare.Contracts.Reminders
{
    public class TodoRequest
    {
      public  string? Description { get; set; } = string.Empty;

        public TimeOnly? Date { get; set; } 
       
    }
}
