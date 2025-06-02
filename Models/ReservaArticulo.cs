using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class ReservaArticulo
{
    public long Id { get; set; }

    public long? ReservaId { get; set; }

    public long? ArticuloId { get; set; }

    public DateOnly? FechaReserva { get; set; }

    public DateOnly? FechaDevolucion { get; set; }

    public long? EstadoId { get; set; }

    public int? Cantidad { get; set; }

    public virtual Articulo? Articulo { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual Reserva? Reserva { get; set; }
}
