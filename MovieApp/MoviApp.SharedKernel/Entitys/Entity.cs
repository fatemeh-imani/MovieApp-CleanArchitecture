using MovieApp.SharedKernel.Event;

namespace MoviApp.SharedKernel.Entitys
{
    public  abstract class Entity
    {
        public Guid Id { get; protected set; }
        public bool IsDelete { get;  set; }

        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyList<IDomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();
        
        protected Entity(Guid id)
        {
            Id = id;
        }

        public void Delete()
        {
            IsDelete = true;

        }

        public void Restore()
        {
            IsDelete = false;
        }

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);

        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
