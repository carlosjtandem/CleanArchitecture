namespace CleanArchitecture.Domain.Vehiculos;

public abstract class Entity  // Abstract porque no puede crear objetos, pero los hijos que se hereden si lo podran
{
    //Cuando queramos crear una entidad se creará enviando este id
    protected Entity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }  // init significa que una vez que se cree el identificador nunca cambiará
}