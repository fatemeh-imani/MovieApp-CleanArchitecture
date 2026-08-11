

using MoviApp.SharedKernel.Errors;

namespace MovieApp.Domain.Entitys.Ratings
{
    public static class RatingErrors
    {
        public static readonly Error NotFound = new(
            "Rating.NotFound",
            "Rating Was Not Found",
            ErrorType.NotFound);

        public static readonly Error InvalidScore = new(
            "Rating.InvalidScore",
            "Rating Score Must be Between 1 And 10",
            ErrorType.Validation);

        public static readonly Error AlreadyExists = new(
            "Rating.AlreadyExists",
            "User Already rated this Movie",
            ErrorType.Conflict);
    }
}
