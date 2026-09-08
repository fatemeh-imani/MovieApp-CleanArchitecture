using MediatR;
using Microsoft.Extensions.Logging;
using MovieApp.Domain.Entitys.Movies.Event;

namespace MovieApp.Application.Movies.Events;

internal sealed class MovieCreatedDomainEventHandler(
       ILogger<MovieCreatedDomainEventHandler> _logger)
    : INotificationHandler<MovieCreatedDomainEvent>
{
    public Task Handle(
        MovieCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Movie {MovieId} was created.", notification.MovieId);

        return Task.CompletedTask;
    }
}