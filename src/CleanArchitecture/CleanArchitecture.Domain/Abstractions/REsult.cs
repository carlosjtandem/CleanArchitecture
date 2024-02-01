using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace CleanArchitecture.Domain.Abstractions;

public class Result
{  // Esta clase retorna un success o error

    protected internal Result(bool isSuccess, Error error)  //solo puede ser llamados por los hijos de esta clase
    {
        // no puede ser exitoso y a la vez tener error entonces lo debemos controlar
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }   // solo para consulta
    public bool IsFailure => !IsSuccess;  // es la negacion del success
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);  //los parametros enviados true= es Success y Erro.none=no hay errores 

    //En caso de que haya errores
    public static Result Failure(Error error) => new(false, error);

    //Metodo q devuelve un sucess bajo la categoria de genricos
    public static Result<TValue> Success<TValue>(TValue value)
    => new(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error)
    => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value)
    => value is not null
    ? Success(value)
    : Failure<TValue>(Error.NullVAlue);
}

public class Result<TValue> : Result
{  // una clase generica por eso usamos TValue es decir podemos pasar cualquier objeto
    private readonly TValue? _value;
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)  //base :xxxx siginifica que implementa tambien el constructor del padre (Result)
    {
        _value = value;
    }


    [NotNull]  // notacion de system.codeanalysis
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("El resultado del valor de error no es admisible");

    public static implicit operator Result<TValue>(TValue value) => Create(value);

}