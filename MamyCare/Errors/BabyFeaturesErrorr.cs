namespace MamyCare.Errors
{
    public static class BabyFeaturesErrorr
    {

        public static readonly Error NullArticles =
     new("", "there is no articles with this id", StatusCodes.Status400BadRequest);

        public static readonly Error EmptyGallary =
  new("", "there is no Data in this request", StatusCodes.Status400BadRequest);
        public static readonly Error Gallary =
new("", "there are no images to show", StatusCodes.Status204NoContent);
        public static readonly Error Nomatchedbaby =
new("", "no matched baby", StatusCodes.Status400BadRequest);
    }
}

