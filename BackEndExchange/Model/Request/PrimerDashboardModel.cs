using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class PrimerDashboardModel
  {
    public int idUsuario { get; set; }
    public int idBilletera { get; set;}
    public string simbolo { get; set; }
    public string criptomoneda { get; set; }
    public decimal? precioCompra { get; set; }
    public decimal? cantidad { get; set; }
  }
}
