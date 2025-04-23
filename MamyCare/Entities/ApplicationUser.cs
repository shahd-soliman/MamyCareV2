

namespace MamyCare.Entities
{
    public class ApplicationUser: IdentityUser<int>
    {
        
        public Mother Mother { get; set; }
    }
}
