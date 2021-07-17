using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.PropositoGeneral
{
  public class ConfiguracionCompraModel
  {
    public double PrecioVenta { get; set; }
    public double Comision { get; set; }
    public int IdTipoMovimiento { get; set; }
    public int IdCriptomoneda { get; set; }
    public string NombreCriptomoneda { get; set; }

  }
}
