using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres;

public static class AlquilerErrors
{
    public static Error NotFound = new Error(    // referencia a nuestra clase personalizada Error
        "Alquuler.Found",
        "El alquiler con el id especificado no fue encontrado"
    );

    //Cuando alguien previamente ya reservo entonces usaré la siguiente clase.
    public static Error Overlap = new Error(
        "Alquiler.Overlap",
        "El alquiler está siendo tomado por dos clientes al mismo tiempo en la misma fecha"
    );

    public static Error NotReserved = new Error(
        "Alquiler.NotReserved",
        "El alquiler no está reservado"
    );

    public static Error NotConfirmado = new Error(
        "Alquiler.NotConfirmed",
        "El alquiler no está confirmado"
    );

    public static Error AlreadyStarted = new Error(
        "Alquiler.AlreadyStarted",
        "El alquiler ya ha comenzado"
    );
}