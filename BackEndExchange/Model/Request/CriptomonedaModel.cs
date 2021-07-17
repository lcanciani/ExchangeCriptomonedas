using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class CriptomonedaModel
    {
    public int IdCriptomoneda { get; set; }
    public string Nombre { get; set; }
    public double PrecioCompra { get; set; }
    public double StockDisponible { get; set; }
    public string Simbolo { get; set; }
    public double StockTotal { get; set; }
    public double PorcentajeGanancia { get; set; }
    public string ImagenUrl { get; set; }
    public DateTime? FechaBaja { get; set; }
  }
}
