namespace CleanArchitecture.Domain.Users;

public interface IUserRepository
{

    //El requerimiento es tener un metodo para obtener un usuario por UserId  y el otro es para persistir un nuevo usuario de la clase entidad (pero aun no guarda en la DB)
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(User user);
}