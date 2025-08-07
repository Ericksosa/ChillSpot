using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Estado
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Descuento> Descuentos { get; set; } = new List<Descuento>();

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();

    public virtual ICollection<Idioma> Idiomas { get; set; } = new List<Idioma>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<MetodoPago> MetodoPagos { get; set; } = new List<MetodoPago>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Penalizacion> Penalizacions { get; set; } = new List<Penalizacion>();

    public virtual ICollection<Renta> Renta { get; set; } = new List<Renta>();

    public virtual ICollection<ReservaArticulo> ReservaArticulos { get; set; } = new List<ReservaArticulo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<RolProfesional> RolProfesionals { get; set; } = new List<RolProfesional>();

    public virtual ICollection<TipoArticulo> TipoArticulos { get; set; } = new List<TipoArticulo>();
}
