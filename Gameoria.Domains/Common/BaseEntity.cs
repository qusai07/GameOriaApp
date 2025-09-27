using System.ComponentModel.DataAnnotations.Schema;


namespace GameOria.Domains.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        private readonly List<BaseEntity> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEntity> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEntity domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEntity domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    }
}
