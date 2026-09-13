using FluentValidation;
using MediatR;
using MovieApp.SharedKernel.Errors;
using MovieApp.SharedKernel.Results;

namespace MovieApp.Application.Behaviors
{
    internal sealed class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> _validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : Result
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(
                    x => x.ValidateAsync(context,cancellationToken)));
              
            var errors = validationResults
                .SelectMany(error => error.Errors)
                .Where(error => error is not null)
                .Select(error => Error.Validation(
                   error.ErrorCode,
                   error.ErrorMessage,
                   ErrorType.Validation))
                .ToList();

            if(errors.Count != 0)
            {
               return (TResponse)(object)ValidationResult.Failure(errors);
            }

            return await next();
        }
    }
}
