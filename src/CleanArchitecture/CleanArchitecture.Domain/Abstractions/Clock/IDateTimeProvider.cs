namespace CleanArchitecture.Domain.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime currentTime { get; }
}