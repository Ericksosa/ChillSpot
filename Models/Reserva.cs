using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Reserva
{
    public long Id { get; set; }

    public long? ArticuloId { get; set; }

    public long? EstadoId { get; set; }

    public long? ClienteId { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public long? IdDescuento { get; set; }

    public int? DuracionReserva { get; set; }

    public long? EmpleadoId { get; set; }

    public decimal? MontoTotal { get; set; }

    public long? PenalizacionId { get; set; }

    public virtual Articulo? Articulo { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual Empleado? Empleado { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual Descuento? IdDescuentoNavigation { get; set; }

    public virtual Penalizacion? Penalizacion { get; set; }

    public virtual ICollection<Rentum> Renta { get; set; } = new List<Rentum>();

    public virtual ICollection<ReservaArticulo> ReservaArticulos { get; set; } = new List<ReservaArticulo>();
}
