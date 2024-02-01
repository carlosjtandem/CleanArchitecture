using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity  // sealed porque no queremos que esta clase sea heredada

{
    public Vehiculo(Guid id,
    Modelo modelo,
    Vin vin,
    Moneda precio,
    Moneda mantenimiento,
    DateTime? fechaUltimaAlquiler,
    List<Accesorio> accesorios,
    Direccion? direccion
    ) : base(id)  //base y enviamos id porque la clase padre Entity requiere como su constructo un id
    {
        //vamos a seterlo al interior de la clase
        Modelo = modelo;
        Vin = vin;
        Precio = precio;
        Mantenimiento = mantenimiento;
        FechaUltimaAlquiler = fechaUltimaAlquiler;
        Accesorios = accesorios;
        Direccion = direccion;
    }
    //el siguiente campo lo manejamos como object value porque no existe dos modelos similares, es decir existe un unico modulo.
    public Modelo? Modelo { get; private set; } // private set significa que solo por esta clase se puede actualizar el valor
    public Vin? Vin { get; private set; }// VIn es un serail number entonces lo vamos a manejar como object value.
    public Direccion? Direccion { get; private set; }
    public Moneda? Precio { get; private set; }
    public Moneda? Mantenimiento { get; private set; }
    public DateTime? FechaUltimaAlquiler { get; internal set; }
    public List<Accesorio>? Accesorios { get; private set; }
}