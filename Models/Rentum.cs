using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Rentum
{
    public long Id { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public long EstadoId { get; set; }

    public long ReservaId { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual Reserva Reserva { get; set; } = null!;
}
