using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users.Events;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity
{
    private User()
    {

    }
    private User(
        Guid id,
        Nombre nombre,
        Apellido apellido,
        Email email
    ) : base(id)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
    }
    //como estoy trabajando con DDD entonces mi entidad va modelada con bject values
    public Nombre? Nombre { get; private set; } // Cualquier agente externo puede consultar el valor de la popiedad,, y private set significa que ningun agente externo ppuede modificar el valor de la propiedad. La unica forma es al crearlo llamando a la clase en el constructor.
    public Apellido? Apellido { get; private set; }
    public Email? Email { get; private set; }

    //Este metoo será llamado cuabndo vayamoa a crer un nuevo usuario. .. Esto se hace porque se quiere ocultar el constructor.
    public static User Create
    (
        Nombre nombre,
        Apellido apellido,
        Email email
    )
    {
        var user = new User(Guid.NewGuid(), nombre, apellido, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id)); //aqui estamos llamando a un evento.. lo estamos publicando 
        return user;
    }
}