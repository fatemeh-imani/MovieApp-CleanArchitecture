

using MoviApp.SharedKernel.Errors;

namespace MovieApp.Domain.Entitys.Genres
{
    public static class GenreErrors
    {
        public static readonly Error NotFound = new(
          "Genre.NotFound",
          "Genre Eas Not Found",
          ErrorType.NotFound);

        public static readonly Error AlreadyExists = new(
            "Genre.AlreadyExists",
            "Genre Already Exists",
            ErrorType.Conflict);

        public static readonly Error InvalidTitle = new(
            "Genre.InvalidTitle",
            "Genre Invalid Title",
            ErrorType.Validation);
    }
}
