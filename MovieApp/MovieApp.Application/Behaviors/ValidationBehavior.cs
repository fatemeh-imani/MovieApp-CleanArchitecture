
using FluentValidation;
using MediatR;

namespace MovieApp.Application.Behaviors
{
    internal sealed class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> _validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
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

            var failures = await Task.WhenAll(
                _validators.Select(
                    x => x.ValidateAsync(context,cancellationToken)));
              
            var errors = failures
                .SelectMany(x => x.Errors)
                .Where(x =>x is not null)
                .ToList();

            if(errors.Count != 0)
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
