
using MovieApp.SharedKernel.Event;

namespace MovieApp.Domain.Entitys.Movies.Event
{
    public sealed record MovieCreatedDomainEvent(Guid MovieId):IDomainEvent;
    
}
