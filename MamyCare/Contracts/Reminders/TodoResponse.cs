namespace MamyCare.Contracts.Reminders
{
    public class TodoResponse
    {
      public  int id { get; set; }
        public  string? Description { get; set; }
        public  bool ? Isdone { get; set; }
        public TimeOnly? Date { get; set; }
         
    }
}
