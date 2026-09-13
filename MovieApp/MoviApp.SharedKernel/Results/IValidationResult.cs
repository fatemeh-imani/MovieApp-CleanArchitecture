
using MovieApp.SharedKernel.Errors;

namespace MovieApp.SharedKernel.Results
{
    public interface IValidationResult
    {
        IReadOnlyList<Error> Errors { get; }
    }
}
