using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class RedPago
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public long EstadoId { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
