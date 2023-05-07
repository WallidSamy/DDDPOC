namespace DDDPOC.SharedKernel.Interfaces
{
    public abstract class AggregateRoot<TId> : BaseEntity<TId>
    {
        private List<BaseDomainEvent> _events = new();

        public void RaiseDomainEvent(BaseDomainEvent domainEvents)
        {
            _events.Add(domainEvents);
        }

        public List<BaseDomainEvent> GetAllEvents()
        {
            return _events;
        }

    }
}
