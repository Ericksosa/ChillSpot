using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Empleado
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public long Cedula { get; set; }

    public string TandaLabor { get; set; } = null!;

    public decimal? PorcientoComision { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public long EstadoId { get; set; }

    public long UsuarioId { get; set; }

    public virtual ICollection<Descuento> Descuentos { get; set; } = new List<Descuento>();

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual Usuario Usuario { get; set; } = null!;
}
