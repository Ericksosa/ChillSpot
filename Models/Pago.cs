using System;
using System.Collections.Generic;

namespace ChillSpot.Models;

public partial class Pago
{
    public long Id { get; set; }

    public long? MetodoPagoId { get; set; }

    public decimal MontoApagar { get; set; }

    public long EstadoId { get; set; }

    public long NumTarjeta { get; set; }

    public long FechaVencimientoTarjeta { get; set; }

    public int Cvv { get; set; }

    public long? RedPagoId { get; set; }

    public string NombreDuenioTarjeta { get; set; } = null!;

    public long? DescuentoId { get; set; }

    public virtual Descuento? Descuento { get; set; }

    public virtual Estado? MetodoPago { get; set; }

    public virtual MetodoPago? MetodoPagoNavigation { get; set; }

    public virtual RedPago? RedPago { get; set; }
}
