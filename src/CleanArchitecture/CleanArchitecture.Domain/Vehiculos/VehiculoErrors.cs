using CleanArchitecture.Domain.Abstractions;

namespace CleaArchitecture.Domain.Vehiculos;

public static class VehiculoErrors
{

    public static Error NotFound = new(
        "Vehiculo.Found",
        "No existe el usuario por el id"
    );

}