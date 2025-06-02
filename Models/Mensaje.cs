using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Mensaje
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public long EstadoId { get; set; }

    public virtual Estado Estado { get; set; } = null!;
}
