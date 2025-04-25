namespace MamyCare.Errors
{
    public static class BabyFeaturesErrorr
    {

        public static readonly Error NullArticles =
 new("", "there is no articles with this id", StatusCodes.Status400BadRequest);

       
    }
}

