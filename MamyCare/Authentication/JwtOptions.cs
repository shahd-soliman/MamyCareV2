using System.ComponentModel.DataAnnotations;

namespace MamyCare.Authentication
{
    public class Jwtoptions
    {
        public static string SectionName = "JWT";
        [Required]
        public string Key { get; init; }=string.Empty;
        [Required]
        public string Issuer { get; init; } = string.Empty;
        [Required]
        public string Audience { get; init; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ExpiryMinutes must be >= 1")]
        public int ExpireMinutes { get; set; }
    }
}
