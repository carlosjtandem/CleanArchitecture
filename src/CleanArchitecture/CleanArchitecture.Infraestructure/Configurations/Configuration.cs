using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations;

internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("vehiculos"); // aqui se indica el nombre que se creará en la DB
        builder.HasKey(vehiculo => vehiculo.Id); //PK

        builder.OwnsOne(vehiculo => vehiculo.Direccion); //

        builder.Property(vehiculo => vehiculo.Modelo)  //Modelo es un object value y se lo debe pasar a Postgress como un valor primitivo para que PG lo entienda
        .HasMaxLength(200)
        .HasConversion(modelo => modelo!.Value, value => new Modelo(value));

        builder.Property(vehiculo => vehiculo.Vin)
        .HasMaxLength(500)
        .HasConversion(vin => vin!.Value, value => new Vin(value));  // el Vin se pasa como string

        builder.OwnsOne(vehiculo => vehiculo.Precio,
        priceBuilder =>
        {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
        .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        }); //

        builder.OwnsOne(vehiculo => vehiculo.Mantenimiento, priceBuilder =>
        {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

    }
}