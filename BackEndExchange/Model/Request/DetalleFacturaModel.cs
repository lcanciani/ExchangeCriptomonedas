using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class DetalleFacturaModel
    {

    public int IdDetalleFactura { get; set; }
    public int? IdFactura { get; set; }
    public decimal? Precio { get; set; }
    public int? IdCriptomoneda { get; set; }
    public decimal? Cantidad { get; set; }
    public decimal? CotizacionDolar { get; set; }
    public decimal? Comision { get; set; }
    public decimal? PorcentajeGanancia { get; set; }

  }
}
