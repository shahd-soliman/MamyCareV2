using MamyCare.Contracts.Authentication;

namespace MamyCare.Contracts.User
{
    public class UpdateProfileRequestvalidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestvalidator()
        {
           
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.phone).NotEmpty();
        }
    }
}

