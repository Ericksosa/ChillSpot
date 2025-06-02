using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class ArticuloElenco
{
    public long Id { get; set; }

    public long? ArticuloId { get; set; }

    public long? ElencoId { get; set; }

    public int? RolEnArticulo { get; set; }

    public virtual Articulo? Articulo { get; set; }

    public virtual Elenco? Elenco { get; set; }
}
