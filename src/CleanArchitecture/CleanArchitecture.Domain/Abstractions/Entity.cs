namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity  // Abstract porque no puede crear objetos, pero los hijos que se hereden si lo podrán
{
    private readonly List<IDomainsEvent> _domainEvents = new();

    //Cuando queramos crear una entidad se creará enviando este id
    protected Entity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }  // init significa que una vez que se cree el identificador nunca cambiará

    public IReadOnlyList<IDomainsEvent> GetDomainsEvents()
    {
        return _domainEvents.ToList();
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainsEvent domainsEvent)
    {
        _domainEvents.Add(domainsEvent);
    }
}