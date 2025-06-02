using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Devolucion
{
    public long Id { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly FechaDevolucion { get; set; }

    public long EstadoId { get; set; }

    public long ReservaId { get; set; }

    public long PenalizacionId { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual Penalizacion Penalizacion { get; set; } = null!;

    public virtual Reserva Reserva { get; set; } = null!;
}
