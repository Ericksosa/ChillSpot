using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Articulo
{
    public long Id { get; set; }

    public string? Titulo { get; set; }

    public long? TipoArticuloId { get; set; }

    public long? IdiomaId { get; set; }

    public decimal? RentaXdia { get; set; }

    public decimal? MontoEntregaTardia { get; set; }

    public long? EstadoId { get; set; }

    public long? GeneroId { get; set; }

    public string? Sinopsis { get; set; }

    public int? AnioEstreno { get; set; }

    public string? PortadaUrl { get; set; }

    public string? RutaUrl { get; set; }

    public virtual ICollection<ArticuloElenco> ArticuloElencos { get; set; } = new List<ArticuloElenco>();

    public virtual Estado? Estado { get; set; }

    public virtual Genero? Genero { get; set; }

    public virtual Idioma? Idioma { get; set; }

    public virtual ICollection<ReservaArticulo> ReservaArticulos { get; set; } = new List<ReservaArticulo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual TipoArticulo? TipoArticulo { get; set; }
}
