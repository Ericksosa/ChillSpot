using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Descuento
{
    public long Id { get; set; }

    public string? Descripcion { get; set; }

    public int MontoCobertura { get; set; }

    public long? EmpleadoCreadorId { get; set; }

    public long EstadoId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public virtual Empleado? EmpleadoCreador { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
