namespace CleanArchitecture.Application.Exceptions;

public sealed class ConcurrencyException : Exception //Hereda de excepcion que es una clase propia del SDK
{
    
    public ConcurrencyException(string message, Exception innerException)
    : base(message, innerException)
    {

    }
}