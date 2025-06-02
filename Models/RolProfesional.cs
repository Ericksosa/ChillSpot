using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class RolProfesional
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public long EstadoId { get; set; }

    public virtual ICollection<Elenco> Elencos { get; set; } = new List<Elenco>();

    public virtual Estado Estado { get; set; } = null!;
}
