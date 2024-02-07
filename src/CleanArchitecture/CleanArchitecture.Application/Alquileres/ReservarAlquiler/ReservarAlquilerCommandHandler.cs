using CleaArchitecture.Application.Abstractions.Messaging;
using CleaArchitecture.Domain.Users;
using CleaArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Abstractions.Clock;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleaArchitecture.Application.Alquileres.ReservarAlquiler;

internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
{

    private readonly IUserRepository _userRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IAlquilerRepository _alquilerRepository;
    private readonly PrecioService _precioService;
    private readonly IUnitOfWork _unitOFWork;
     private readonly IDateTimeProvider _dateTimeProvider;

    public ReservarAlquilerCommandHandler(
        IUserRepository userRepository,
        IVehiculoRepository vehiculoRepository,
        IAlquilerRepository alquilerRepository,
        PrecioService precioService,
        IUnitOfWork unitOFWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _vehiculoRepository = vehiculoRepository;
        _alquilerRepository = alquilerRepository;
        _precioService = precioService;
        _unitOFWork = unitOFWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        ReservarAlquilerCommand request,
        CancellationToken cancellationToken
        )
    {
        //Buscar al usuario
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        //Vehiculo
        var vehiculo = await _vehiculoRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (vehiculo is null)
        {
            return Result.Failure<Guid>(VehiculoErrors.NotFound);
        }

        //calculo de dias que se renta el vehiculo
        var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);

        //overlaping -- para evitar que se pueda alquilar el dia cuando ya fue asignado
        if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }

        //si pasamos las validaciones-- entonces ya puedo crear el alquiler
        var alquiler = Alquiler.Reservar(vehiculo,
        user.Id,
        duracion,
        _dateTimeProvider.currentTime,  // DateTime.UtcNow,  // lo vamos a hacer con una interfaz , esto se hace para que sea testeable con unit mocks
        _precioService
        );

        //ya podemos agregar el alquiler al repositorio
        await _unitOFWork.SaveChangesAsync(cancellationToken);

        return alquiler.Id;
    }


}