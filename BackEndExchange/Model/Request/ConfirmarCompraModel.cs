using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class ConfirmarCompraModel
  {
    public int IdUsuario { get; set; }
    public int IdCriptomoneda { get; set; }
    public int IdTipoMovimiento { get; set; }
    public string NombreCriptomoneda { get; set; }
    public double PrecioVenta { get; set; }
    public double Comision { get; set; }
    public double CotizacionDolar { get; set; }
    public double Cantidad { get; set; }
    public double Monto { get; set; }
    public double PorcentajeGanancia { get; set; }
  }
}
