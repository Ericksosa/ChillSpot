using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Idioma
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public long? EstadoId { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    public virtual Estado? Estado { get; set; }
}
