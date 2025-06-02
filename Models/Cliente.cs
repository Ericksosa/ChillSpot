using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Cliente
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Cedula { get; set; }

    public string? TarjetaCr { get; set; }

    public decimal? LimiteCredito { get; set; }

    public string? TipoPersona { get; set; }

    public long? EstadoId { get; set; }

    public long? UsuarioId { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual Usuario? Usuario { get; set; }
}
