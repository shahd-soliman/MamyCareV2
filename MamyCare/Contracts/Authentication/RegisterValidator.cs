namespace MamyCare.Contracts.Authentication
{
    public class RegisterValidator : AbstractValidator<RequestRegister>
    {
        private List<string> _AllowedExtentions = new() { ".jpg", ".jpeg", ".png " };
        private int MaxSize = 2 * 1024;
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.babyName).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
           
            RuleFor(x=> x.gender).NotEmpty();

            RuleFor(x => x.MotherImage)
                     .Must(ValidateFile)
                     .WithMessage($"Image must be less than {MaxSize / 1024}MB and one of these extensions: {string.Join(", ", _AllowedExtentions)}");
        }

        private bool ValidateFile(IFormFile file)
        {
            if (file == null)
                return true; 

           
            if (file.Length > MaxSize * 1024)
                return false;

            
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_AllowedExtentions.Contains(extension))
                return false;

            return true;
        }
    }
}
