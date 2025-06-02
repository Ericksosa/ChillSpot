using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Elenco
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public long? RolProfesionalId { get; set; }

    public virtual ICollection<ArticuloElenco> ArticuloElencos { get; set; } = new List<ArticuloElenco>();

    public virtual RolProfesional? RolProfesional { get; set; }
}
