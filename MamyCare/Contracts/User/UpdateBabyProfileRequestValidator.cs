namespace MamyCare.Contracts.User
{
    public class UpdateBabyProfileRequestValidator : AbstractValidator<UpdateBabyProfileRequest>
    {
        public UpdateBabyProfileRequestValidator()
        {
            RuleFor(x => x.BabyName).NotEmpty().WithMessage("Baby Name is required");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Birth Date is required");
            

        }
    }
}
