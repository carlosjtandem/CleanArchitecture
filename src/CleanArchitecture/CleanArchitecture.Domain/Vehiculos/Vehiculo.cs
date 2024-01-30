namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity  // sealed porque no queremos que esta clase sea heredada

{
    public Vehiculo(Guid id) : base(id)
    {

    }
    public string? Modelo { get; private set; } // private set significa que solo por esta clase se puede actualizar el valor
    public string? Vin { get; private set; }
    public Direccion Direccion { get; private set; }
    public decimal? Precio { get; private set; }
    public string? TipoMoneda { get; private set; }
    public decimal? Mantenimiento { get; private set; }
    public string? MantenimientoTipoMoneda { get; private set; }
    public DateTime? FechaUltimoAlquiler { get; private set; }

    public List<Accesorio> Accesorios { get; private set; }
}