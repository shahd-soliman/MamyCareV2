namespace MamyCare.Errors
{
    public static class HospitalErrors
    {
        public static readonly Error InvalidHospital =
      new("", "Invalid Hospital", StatusCodes.Status401Unauthorized);
        public static readonly Error NoFAvouriteHospitals =
    new("", "NoFAvouriteHospitals ", StatusCodes.Status204NoContent);
        
                 public static readonly Error DuplicateFavouriteHopital =
    new("", "the Hospital Is Already Favourite ", StatusCodes.Status204NoContent);
    }
}

