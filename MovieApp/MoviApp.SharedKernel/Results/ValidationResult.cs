using MovieApp.SharedKernel.Errors;

namespace MovieApp.SharedKernel.Results
{
    public sealed class ValidationResult(IReadOnlyList<Error> errors) 
        : Result(false, errors.First()), IValidationResult 
    {
        public IReadOnlyList<Error> Errors { get; } = errors;

        public static ValidationResult Failure(
                IReadOnlyList<Error> errors)
        {
            return new ValidationResult(errors);
        }
    }
}
