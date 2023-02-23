namespace domain;


public abstract class AggregateRoot : IAggregateRoot
{
    private List<IDomainEvent> _domainEvents = new();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public List<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
interface IAggregateRoot{}