using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class DepositoModel
  {
    public int IdUsuario { get; set; }
    public decimal? Monto { get; set; }
    public int? IdBanco { get; set; }
    public int IdTipoMovimiento { get; set; }
    public decimal? comision { get; set; }
  }
}
