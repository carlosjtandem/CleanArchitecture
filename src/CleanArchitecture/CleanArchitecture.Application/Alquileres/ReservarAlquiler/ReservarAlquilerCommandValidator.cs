using FluentValidation;

namespace CleaArchitecture.Application.Alquileres.ReservarAlquiler;

public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommand>
{
    public ReservarAlquilerCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VehiculoId).NotEmpty();
        RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin); // La fecha de inicio tiene que ser menos que la fecha final
    }
}