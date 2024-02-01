using System.Security.Permissions;

namespace CleanArchitecture.Domain.Abstractions;

public record Error(string Code, string Name)
{
    //Dos propiedades estaticas
    public static Error None = new Error(string.Empty, string.Empty);  // Genera un objecto tipo Error en blanco
    public static Error NullVAlue = new("Error.NullVAlue", "Un valor null fue ingresado");
}