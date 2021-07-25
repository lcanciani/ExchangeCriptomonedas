using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class PrimerDashboardModel
  {
    public int idUsuario { get; set; }
    public string nombreUsuario { get; set; }
    public decimal? saldo { get; set; }
    public string criptomoneda { get; set; }
    public decimal? precioCompra { get; set; }
    public decimal? cantidad { get; set; }
  }
}
