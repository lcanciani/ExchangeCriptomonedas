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
    public decimal? PrecioCompra { get; set; }
    public decimal? Stock { get; set; }
    public string Simbolo { get; set; }
    public decimal? Capitalizacion { get; set; }
    public decimal? ValorTotal { get; set; }
    public decimal? PrecioVenta { get; set; }

    public string ImagenUrl { get; set; }
  }
}
