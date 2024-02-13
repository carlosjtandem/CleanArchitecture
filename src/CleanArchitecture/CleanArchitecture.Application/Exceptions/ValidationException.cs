namespace CleanArchitecture.Application.Exceptions;

public sealed class ValidationException : Exception  //Hereda de excepcion que es una clase propia del SDK
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }
    public IEnumerable<ValidationError> Errors{get;}
}