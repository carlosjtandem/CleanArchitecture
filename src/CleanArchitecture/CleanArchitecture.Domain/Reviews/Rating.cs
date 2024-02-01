using CleanArchitecture.Domain.Abstractions;

namespace CleaArchitecture.Domain.Reviews;

public sealed record Rating  //Es la calificacion que se da a la revision es del 1 a 5
{

    public static readonly Error Invalid = new("Rating.Invalid","El rating es invalido");  // este viene referenciado de nuestro error  
       
    public int Value {get; init;}  // propiedad que me permite saber el valor del rating

    // private Rating(int value) 
    // {
    //     Value = value;
    // }
    private Rating(int value) => Value = value;  //Este ctor es == Recibe el valor y lo inicializa  -- otra forma de hacerlo es como las 3 lineas de arriba
    
    public static Result<Rating> Create(int value)
    {
        if(value < 1 || value > 5)
        {
            return Result.Failure<Rating>(Invalid);  //Retorna el error creado en la linea 8
        }

        return new Rating(value);
    }

}