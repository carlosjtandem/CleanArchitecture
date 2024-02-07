using CleanArchitecture.Domain.Abstractions;

namespace CleaArchitecture.Domain.Users;

public static class UserErrors
{

    public static Error NotFound = new(
        "User.Found",
        "No existe el usuario por el id"
    );

    public static Error InvalidCredentials = new(
        "User.InvalidCredentiales",
        "Las credenciales son incorrectas"
);
}