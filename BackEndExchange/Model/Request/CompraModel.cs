using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class CompraModel
  {
    public int IdFactura { get; set; }
    public DateTime? Fecha { get; set; }
    public int? IdUsuario { get; set; }

    
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }

  }
}
