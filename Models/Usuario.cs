using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public long? RolId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Rol? Rol { get; set; }
}
