using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Penalizacion
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public decimal Monto { get; set; }

    public string? Descripcion { get; set; }

    public long EstadoId { get; set; }

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
