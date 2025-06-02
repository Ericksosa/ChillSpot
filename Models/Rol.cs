using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Rol
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
