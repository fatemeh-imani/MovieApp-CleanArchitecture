using MoviApp.SharedKernel.Errors;

namespace MovieApp.Domain.Entitys.Movies
{
    public static class MovieErrors
    {
        public static  Error NotFound(Guid id) =>
            Error.NotFound(
                "Movie.NotFound",
                $"The Movie with id '{id}' Was Not Found",
                ErrorType.NotFound);

        public static readonly Error AlreadyExists = new (
               "Movie.AlreadyExists",
               "Movie Already Exists",
               ErrorType.Conflict);

        public static readonly Error InvalidReleaseYear = new(
            "Movie.InvalidReleaseYear",
            "Movie Release year is Invalid ",
            ErrorType.Validation);

        public static readonly Error InvalidTitle = new(
            "Movie.InvalidTitle",
            "Movie title Can Not be Empty",
            ErrorType.Validation);

        public static readonly Error GenreNotFound = new(
            "Movie.GenreNotFound",
            "Genre Not Found",
            ErrorType.NotFound);

        public static readonly Error AlreadyExistsGenre = new(
           "Movie.AlreadyExistsGenre",
           "Genres Already Exists",
           ErrorType.Conflict);

        public static readonly Error AlreadyExistsRated = new(
           "Movie.AlreadyExistesRated",
           "Rated Already Exists",
           ErrorType.Conflict);
    }
}
