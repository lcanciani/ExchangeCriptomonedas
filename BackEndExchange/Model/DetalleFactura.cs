using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public int? IdFactura { get; set; }
        public decimal? Precio { get; set; }
        public int? IdCriptomoneda { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? CotizacionDolar { get; set; }
        public decimal? Comision { get; set; }
        public decimal? PorcentajeGanancia { get; set; }
        public decimal? Monto { get; set; }

        public virtual Criptomoneda IdCriptomonedaNavigation { get; set; }
        public virtual Factura IdFacturaNavigation { get; set; }
    }
}
