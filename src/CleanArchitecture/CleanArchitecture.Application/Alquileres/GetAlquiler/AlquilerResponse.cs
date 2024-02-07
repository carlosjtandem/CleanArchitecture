namespace CleaArchitecture.Application.Alquileres.GetAlquiler;

public sealed class AlquilerResponse  //ESta es una clase que se envia al cliente(lo ideal es enviar datos primitivos), y no algo complejo como se lo creo en la capa de modelo (Pbject value)
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid VehiculoId { get; init; }
    public int Status { get; init; }
    public decimal PrecioAlquiler { get; init; }
    public string? TipoMonedaAlquiler { get; init; }
    public decimal PrecioMantenimiento { get; init; }
    public string? TipoMonedaMantenimiento { get; init; }
    public decimal AccesoriosPrecio { get; init; }
    public string? TipoMonedaAccesorio { get; init; }
    public decimal PrecioTotal { get; init; }
    public string? TipoMonedaPrecioTotal { get; init; }
    public DateOnly DuracionInicio { get; init; }
    public DateOnly DuracionFinal { get; init; }
    public DateTime FechaCReacion { get; init; }
}