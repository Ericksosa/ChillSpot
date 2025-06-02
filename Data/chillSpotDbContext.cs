using System;
using System.Collections.Generic;
using ChillSpot.Models;
using Microsoft.EntityFrameworkCore;

namespace ChillSpot.Data;

public partial class chillSpotDbContext : DbContext
{
    public chillSpotDbContext()
    {
    }

    public chillSpotDbContext(DbContextOptions<chillSpotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<ArticuloElenco> ArticuloElencos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<Devolucion> Devolucions { get; set; }

    public virtual DbSet<Elenco> Elencos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Penalizacion> Penalizacions { get; set; }

    public virtual DbSet<RedPago> RedPagos { get; set; }

    public virtual DbSet<Rentum> Renta { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<ReservaArticulo> ReservaArticulos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolProfesional> RolProfesionals { get; set; }

    public virtual DbSet<TipoArticulo> TipoArticulos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articulo__3213E83F60C1242E");

            entity.ToTable("Articulo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.GeneroId).HasColumnName("Genero_Id");
            entity.Property(e => e.IdiomaId).HasColumnName("Idioma_Id");
            entity.Property(e => e.MontoEntregaTardia).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.PortadaUrl)
                .HasMaxLength(255)
                .HasColumnName("PortadaURL");
            entity.Property(e => e.RentaXdia)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("RentaXDia");
            entity.Property(e => e.RutaUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RutaURL");
            entity.Property(e => e.Sinopsis).HasColumnType("text");
            entity.Property(e => e.TipoArticuloId).HasColumnName("TipoArticulo_Id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Articulo_Estado");

            entity.HasOne(d => d.Genero).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK_Articulo_Genero");

            entity.HasOne(d => d.Idioma).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdiomaId)
                .HasConstraintName("FK_Articulo_Idioma");

            entity.HasOne(d => d.TipoArticulo).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.TipoArticuloId)
                .HasConstraintName("FK_Articulo_TipoArticulo");
        });

        modelBuilder.Entity<ArticuloElenco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articulo__3213E83F1689B72A");

            entity.ToTable("ArticuloElenco");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticuloId).HasColumnName("Articulo_Id");
            entity.Property(e => e.ElencoId).HasColumnName("Elenco_Id");
            entity.Property(e => e.RolEnArticulo).HasColumnName("RolEn_Articulo");

            entity.HasOne(d => d.Articulo).WithMany(p => p.ArticuloElencos)
                .HasForeignKey(d => d.ArticuloId)
                .HasConstraintName("FK_ArticuloElenco_Articulo");

            entity.HasOne(d => d.Elenco).WithMany(p => p.ArticuloElencos)
                .HasForeignKey(d => d.ElencoId)
                .HasConstraintName("FK_ArticuloElenco_Elenco");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3213E83F90DB1ECF");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.LimiteCredito)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("limiteCredito");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TarjetaCr)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            entity.HasOne(d => d.Estado).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Cliente_Estado");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Cliente_Usuario");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Descuent__3213E83F50491C77");

            entity.ToTable("Descuento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EmpleadoCreadorId).HasColumnName("EmpleadoCreador_Id");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");

            entity.HasOne(d => d.EmpleadoCreador).WithMany(p => p.Descuentos)
                .HasForeignKey(d => d.EmpleadoCreadorId)
                .HasConstraintName("FK_Descuento_Empleado");

            entity.HasOne(d => d.Estado).WithMany(p => p.Descuentos)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Descuento_Estado");
        });

        modelBuilder.Entity<Devolucion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devoluci__3213E83FA31BA99E");

            entity.ToTable("Devolucion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.PenalizacionId).HasColumnName("Penalizacion_Id");
            entity.Property(e => e.ReservaId).HasColumnName("Reserva_Id");

            entity.HasOne(d => d.Estado).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devolucion_Estado");

            entity.HasOne(d => d.Penalizacion).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.PenalizacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devolucion_Penalizacion");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devolucion_Reserva");
        });

        modelBuilder.Entity<Elenco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Elenco__3213E83F0A6716A8");

            entity.ToTable("Elenco");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RolProfesionalId).HasColumnName("RolProfesional_Id");

            entity.HasOne(d => d.RolProfesional).WithMany(p => p.Elencos)
                .HasForeignKey(d => d.RolProfesionalId)
                .HasConstraintName("FK_Elenco_RolProfesional");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3213E83F9B3A782B");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PorcientoComision).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TandaLabor)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            entity.HasOne(d => d.Estado).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Estado");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Usuario");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estado__3213E83F710AAB31");

            entity.ToTable("Estado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3213E83FA292C541");

            entity.ToTable("Genero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Generos)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Genero_Estado");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Idioma__3213E83FE876D2EF");

            entity.ToTable("Idioma");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Idiomas)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Idioma_Estado");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mensajes__3213E83F7DF2BB37");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensajes_Estado");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MetodoPa__3213E83F54BC56F9");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.MetodoPagos)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MetodoPago_Estado");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__3213E83FD69EEE69");

            entity.ToTable("Pago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cvv).HasColumnName("CVV");
            entity.Property(e => e.DescuentoId).HasColumnName("Descuento_Id");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.MetodoPagoId).HasColumnName("MetodoPago_Id");
            entity.Property(e => e.MontoApagar)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("MontoAPagar");
            entity.Property(e => e.NombreDuenioTarjeta)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RedPagoId).HasColumnName("RedPago_Id");

            entity.HasOne(d => d.Descuento).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.DescuentoId)
                .HasConstraintName("FK_Pago_Descuento");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("FK_Pago_Estado");

            entity.HasOne(d => d.MetodoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("FK_Pago_MetodoPago");

            entity.HasOne(d => d.RedPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.RedPagoId)
                .HasConstraintName("FK_Pago_RedPago");
        });

        modelBuilder.Entity<Penalizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Penaliza__3213E83F7D8DECEB");

            entity.ToTable("Penalizacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Penalizacions)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Penalizacion_Estado");
        });

        modelBuilder.Entity<RedPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RedPago__3213E83F54AD491F");

            entity.ToTable("RedPago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Renta__3213E83FDC2286B9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.ReservaId).HasColumnName("Reserva_Id");

            entity.HasOne(d => d.Estado).WithMany(p => p.Renta)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Renta_Estado");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Renta)
                .HasForeignKey(d => d.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Renta_Reserva");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3213E83FB3CE72F4");

            entity.ToTable("Reserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticuloId).HasColumnName("Articulo_Id");
            entity.Property(e => e.ClienteId).HasColumnName("Cliente_Id");
            entity.Property(e => e.EmpleadoId).HasColumnName("Empleado_Id");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.IdDescuento).HasColumnName("Id_Descuento");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PenalizacionId).HasColumnName("Penalizacion_Id");

            entity.HasOne(d => d.Articulo).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ArticuloId)
                .HasConstraintName("FK_Reserva_Articulo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Reserva_Cliente");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("FK_Reserva_Empleado");

            entity.HasOne(d => d.Estado).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Reserva_Estado");

            entity.HasOne(d => d.IdDescuentoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdDescuento)
                .HasConstraintName("FK_Reserva_Descuento");

            entity.HasOne(d => d.Penalizacion).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.PenalizacionId)
                .HasConstraintName("FK_Reserva_Penalizacion");
        });

        modelBuilder.Entity<ReservaArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReservaA__3213E83F14413BC6");

            entity.ToTable("ReservaArticulo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticuloId).HasColumnName("Articulo_Id");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.ReservaId).HasColumnName("Reserva_Id");

            entity.HasOne(d => d.Articulo).WithMany(p => p.ReservaArticulos)
                .HasForeignKey(d => d.ArticuloId)
                .HasConstraintName("FK_ReservaArticulo_Articulo");

            entity.HasOne(d => d.Estado).WithMany(p => p.ReservaArticulos)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_ReservaArticulo_Estado");

            entity.HasOne(d => d.Reserva).WithMany(p => p.ReservaArticulos)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK_ReservaArticulo_Reserva");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83FB4449602");

            entity.ToTable("Rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolProfesional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolProfe__3213E83F80198EB5");

            entity.ToTable("RolProfesional");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.RolProfesionals)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolProfesional_Estado");
        });

        modelBuilder.Entity<TipoArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoArti__3213E83FF9924040");

            entity.ToTable("TipoArticulo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.TipoArticulos)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_TipoArticulo_Estado");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F46B5B3AC");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RolId).HasColumnName("Rol_Id");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
