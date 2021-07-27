using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class ExtraccionModel
  {
     public int IdUsuario { get; set; }
    public double Monto { get; set; }
    public int IdBanco { get; set; }
    public int IdTipoMovimiento { get; set; }
    public double Comision { get; set; }

  }
}
