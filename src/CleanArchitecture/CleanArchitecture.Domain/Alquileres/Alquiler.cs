using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public sealed class Alquiler : Entity
{
    private Alquiler(  // es privado porque solo un metodo estatico puede consumir este metodo
        Guid id,
        Guid vehiculoId,
        Guid userId,
        DateRange duracion,
        Moneda precioPorPeriodo,
        Moneda mantenimiento,
        Moneda accesorios,
        Moneda precioTotal,
        AlquilerStatus status,
        DateTime fechaCreacion) : base(id)  //base y enviamos id porque la clase padre Entity requiere como su constructo un id
    {
        VehiculoId = vehiculoId;
        UserId = userId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPeriodo;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }

    public Guid VehiculoId { get; private set; }
    public Guid UserId { get; private set; }
    public Moneda? PrecioPorPeriodo { get; private set; }//Precio por los dias de alquiler
    public Moneda? Mantenimiento { get; private set; }
    public Moneda? Accesorios { get; private set; }
    public Moneda? PrecioTotal { get; private set; }
    public AlquilerStatus Status { get; private set; }  // Regla de negocio es que un alquiler tiene un conjunto de estados.

    //controlar que la fecha final sea mayor que la fecha inicial y adicional calculcar la cantidad de dias que se va a rentar el automovil

    public DateRange? Duracion { get; private set; }

    public DateTime? FechaCreacion { get; private set; }
    public DateTime? FechaConfirmacion { get; private set; }
    public DateTime? FechaNegacion { get; private set; }
    public DateTime? FechaCompletado { get; private set; }
    public DateTime? FechaCancelacion { get; private set; }


    // el componente que quiera instanciar esta clase solo debe pasar el id
    public static Alquiler Reservar(
        Vehiculo vehiculo,
        Guid userId,
        DateRange duracion,
        DateTime fechaCreacion,
        PrecioService precioService
    )
    {
        var precioDetalle = precioService.CalcularPrecio(
            vehiculo,
            duracion
        );

        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehiculo.Id,
            userId,
            duracion,
            precioDetalle.PrecioPorPeriodo,
            precioDetalle.Mantenimiento,
            precioDetalle.Accesorios,
            precioDetalle.PrecioTotal,
            AlquilerStatus.Reservado,
            fechaCreacion
        );

        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id!));

        //fecha que se realiza el alquiler
        vehiculo.FechaUltimaAlquiler = fechaCreacion;

        return alquiler;
    }


    public Result Confirmar(DateTime utcNow)
    {  // Result es una clase creada que mapeará los registros
        if (Status != AlquilerStatus.Reservado)
        {
            // Se rquiere que se dispare una exception con mensaje de error.. porque el estado es pending.. Solo se puede confiramr cuando está confirmado. (necesito crear un entidad Error)
            return Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Confirmado;
        FechaConfirmacion = utcNow;

        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(Id));

        return Result.Success();
    }

    public Result Rechazar(DateTime utcNow)
    {  // Result es una clase creada que mapeará los registros
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Rechazado;
        FechaNegacion = utcNow;

        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancelar(DateTime utcNow)
    {  // Result es una clase creada que mapeará los registros
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmado);
        }

        //REgla de negocio=> si ya está entregamos el carro y quieres cancelarlo entonces no deberias poder hacerlo porque ya lo estas usando.
        var currentDate = DateOnly.FromDateTime(utcNow);
        if (currentDate < Duracion!.Inicio)
        {
            return Result.Failure(AlquilerErrors.AlreadyStarted);
        }

        Status = AlquilerStatus.Cancelado;
        FechaCancelacion = utcNow;

        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id));

        return Result.Success();
    }

    public Result Completar(DateTime utcNow)
    {  // Result es una clase creada que mapeará los registros
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmado);
        }

        Status = AlquilerStatus.Completado;
        FechaCompletado = utcNow;

        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(Id));

        return Result.Success();
    }

}