using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace CleanArchitecture.Domain.Vehiculos;

//Este record es mas elaborado
public record TipoMoneda
{
    public static readonly TipoMoneda Usd = new("USD");
    public static readonly TipoMoneda Eur = new("EUR");
    public static readonly TipoMoneda None = new("");



    // Se va a pasar un string que puede representar a dolares o euros
    public TipoMoneda(string codigo) => Codigo = codigo;

    public string? Codigo { get; init; }
    public static readonly IReadOnlyCollection<TipoMoneda> All = new[]
    {
        Usd,
        Eur
    };

    //si le paso el codigo de una moneda entonces me debe devolver un objt tipo moneda
    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(c=> c.Codigo == codigo)??
        throw new ApplicationException("El tipo de moneda ingresado es inválido");
    }
};