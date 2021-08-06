using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class MovimientosUsuarioModel
  {
    public DateTime? Fecha { get; set; }
    public string Tipo { get; set; }
    public string Nombre { get; set; }
    public decimal? Cantidad { get; set; }
    public decimal? Monto { get; set; }
    public decimal? Precio { get; set; }
    public int? IdBanco { get; set; }
  }
}
