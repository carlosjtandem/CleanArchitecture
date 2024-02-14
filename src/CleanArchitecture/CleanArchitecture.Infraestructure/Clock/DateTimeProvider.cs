using CleanArchitecture.Application.Abstractions.Clock;

namespace CleanArchitecture.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider  // implementa de la interfaz que se creo en el proyecto Application
{
    public DateTime currentTime => DateTime.UtcNow;
}