using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.PropositoGeneral
{
  public class ReporteComprasModel
  {
    public int? idUsuario { get; set; }
    public string apellido { get; set; }
    public string nombre { get; set; }
    public decimal? montoTotal { get; set; }
  }
}
