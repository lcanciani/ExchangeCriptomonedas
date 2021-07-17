using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class TipoMovimientoModel
  {
    public int IdTiposMovimiento { get; set; }
    public string Tipo { get; set; }
    public double Comision { get; set; }
  }
}
