﻿// <auto-generated />
using System;
using CleaArchitecture.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Infraestructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Domain.Alquileres.Alquiler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("FechaCancelacion")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_cancelacion");

                    b.Property<DateTime?>("FechaCompletado")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_completado");

                    b.Property<DateTime?>("FechaConfirmacion")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_confirmacion");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_creacion");

                    b.Property<DateTime?>("FechaNegacion")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_negacion");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("VehiculoId")
                        .HasColumnType("uuid")
                        .HasColumnName("vehiculo_id");

                    b.HasKey("Id")
                        .HasName("pk_alquileres");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_alquileres_user_id");

                    b.HasIndex("VehiculoId")
                        .HasDatabaseName("ix_alquileres_vehiculo_id");

                    b.ToTable("alquileres", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Reviews.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AlquilerId")
                        .HasColumnType("uuid")
                        .HasColumnName("alquiler_id");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("comentario");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_creacion");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("VehiculoId")
                        .HasColumnType("uuid")
                        .HasColumnName("vehiculo_id");

                    b.HasKey("Id")
                        .HasName("pk_reviews");

                    b.HasIndex("AlquilerId")
                        .HasDatabaseName("ix_reviews_alquiler_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_reviews_user_id");

                    b.HasIndex("VehiculoId")
                        .HasDatabaseName("ix_reviews_vehiculo_id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Apellido")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("apellido");

                    b.Property<string>("Email")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<string>("Nombre")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("nombre");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Vehiculos.Vehiculo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int[]>("Accesorios")
                        .HasColumnType("integer[]")
                        .HasColumnName("accesorios");

                    b.Property<DateTime?>("FechaUltimaAlquiler")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fecha_ultima_alquiler");

                    b.Property<string>("Modelo")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("modelo");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("Vin")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("vin");

                    b.HasKey("Id")
                        .HasName("pk_vehiculos");

                    b.ToTable("vehiculos", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Alquileres.Alquiler", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_alquileres_user_user_id");

                    b.HasOne("CleanArchitecture.Domain.Vehiculos.Vehiculo", null)
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_alquileres_vehiculos_vehiculo_id");

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Moneda", "Accesorios", b1 =>
                        {
                            b1.Property<Guid>("AlquilerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Monto")
                                .HasColumnType("numeric")
                                .HasColumnName("accesorios_monto");

                            b1.Property<string>("TipoMoneda")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("accesorios_tipo_moneda");

                            b1.HasKey("AlquilerId");

                            b1.ToTable("alquileres");

                            b1.WithOwner()
                                .HasForeignKey("AlquilerId")
                                .HasConstraintName("fk_alquileres_alquileres_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Moneda", "Mantenimiento", b1 =>
                        {
                            b1.Property<Guid>("AlquilerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Monto")
                                .HasColumnType("numeric")
                                .HasColumnName("mantenimiento_monto");

                            b1.Property<string>("TipoMoneda")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("mantenimiento_tipo_moneda");

                            b1.HasKey("AlquilerId");

                            b1.ToTable("alquileres");

                            b1.WithOwner()
                                .HasForeignKey("AlquilerId")
                                .HasConstraintName("fk_alquileres_alquileres_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Moneda", "PrecioPorPeriodo", b1 =>
                        {
                            b1.Property<Guid>("AlquilerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Monto")
                                .HasColumnType("numeric")
                                .HasColumnName("precio_por_periodo_monto");

                            b1.Property<string>("TipoMoneda")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("precio_por_periodo_tipo_moneda");

                            b1.HasKey("AlquilerId");

                            b1.ToTable("alquileres");

                            b1.WithOwner()
                                .HasForeignKey("AlquilerId")
                                .HasConstraintName("fk_alquileres_alquileres_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Moneda", "PrecioTotal", b1 =>
                        {
                            b1.Property<Guid>("AlquilerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Monto")
                                .HasColumnType("numeric")
                                .HasColumnName("precio_total_monto");

                            b1.Property<string>("TipoMoneda")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("precio_total_tipo_moneda");

                            b1.HasKey("AlquilerId");

                            b1.ToTable("alquileres");

                            b1.WithOwner()
                                .HasForeignKey("AlquilerId")
                                .HasConstraintName("fk_alquileres_alquileres_id");
                        });

                    b.OwnsOne("DateRange", "Duracion", b1 =>
                        {
                            b1.Property<Guid>("AlquilerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateOnly>("Fin")
                                .HasColumnType("date")
                                .HasColumnName("duracion_fin");

                            b1.Property<DateOnly>("Inicio")
                                .HasColumnType("date")
                                .HasColumnName("duracion_inicio");

                            b1.HasKey("AlquilerId");

                            b1.ToTable("alquileres");

                            b1.WithOwner()
                                .HasForeignKey("AlquilerId")
                                .HasConstraintName("fk_alquileres_alquileres_id");
                        });

                    b.Navigation("Accesorios");

                    b.Navigation("Duracion");

                    b.Navigation("Mantenimiento");

                    b.Navigation("PrecioPorPeriodo");

                    b.Navigation("PrecioTotal");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Reviews.Review", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Alquileres.Alquiler", null)
                        .WithMany()
                        .HasForeignKey("AlquilerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_alquileres_alquiler_id");

                    b.HasOne("CleanArchitecture.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_user_user_id");

                    b.HasOne("CleanArchitecture.Domain.Vehiculos.Vehiculo", null)
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_vehiculos_vehiculo_id");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Vehiculos.Vehiculo", b =>
                {
                    b.OwnsOne("CleanArchitecture.Domain.Vehiculos.Direccion", "Direccion", b1 =>
                        {
                            b1.Property<Guid>("VehiculoId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Ciudad")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direccion_ciudad");

                            b1.Property<string>("Departamento")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direccion_departamento");

                            b1.Property<string>("Pais")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direccion_pais");

                            b1.Property<string>("Provincia")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direccion_provincia");

                            b1.Property<string>("calle")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("direccion_calle");

                            b1.HasKey("VehiculoId");

                            b1.ToTable("vehiculos");

                            b1.WithOwner()
                                .HasForeignKey("VehiculoId")
                                .HasConstraintName("fk_vehiculos_vehiculos_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Moneda", "Mantenimiento", b1 =>
                        {
                            b1.Property<Guid>("VehiculoId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Monto")
                                .HasColumnType("numeric")
                                .HasColumnName("mantenimiento_monto");

                            b1.Property<string>("TipoMoneda")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("mantenimiento_tipo_moneda");

                            b1.HasKey("VehiculoId");

                            b1.ToTable("vehiculos");

                            b1.WithOwner()
                                .HasForeignKey("VehiculoId")
                                .HasConstraintName("fk_vehiculos_vehiculos_id");
                        });

                    b.OwnsOne("CleanArchitecture.Domain.Shared.Moneda", "Precio", b1 =>
                        {
                            b1.Property<Guid>("VehiculoId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Monto")
                                .HasColumnType("numeric")
                                .HasColumnName("precio_monto");

                            b1.Property<string>("TipoMoneda")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("precio_tipo_moneda");

                            b1.HasKey("VehiculoId");

                            b1.ToTable("vehiculos");

                            b1.WithOwner()
                                .HasForeignKey("VehiculoId")
                                .HasConstraintName("fk_vehiculos_vehiculos_id");
                        });

                    b.Navigation("Direccion");

                    b.Navigation("Mantenimiento");

                    b.Navigation("Precio");
                });
#pragma warning restore 612, 618
        }
    }
}
