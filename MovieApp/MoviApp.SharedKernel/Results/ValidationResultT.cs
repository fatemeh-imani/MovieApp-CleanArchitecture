using MovieApp.SharedKernel.Errors;

namespace MovieApp.SharedKernel.Results
{
    public sealed class ValidationResult<T>(
        IReadOnlyList<Error> errors)
        : Result<T>(default, false, errors.First()), IValidationResult
    {
        public IReadOnlyList<Error> Errors { get; } = errors;

        public static ValidationResult<T> Failure(
            IReadOnlyList<Error> errors)
        {
            return new ValidationResult<T>(errors);
        }
    }
}  
