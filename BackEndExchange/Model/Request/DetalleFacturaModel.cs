using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class DetalleFacturaModel
    {

        public int idFactura { get; set; }

        public int idCriptomoneda { get; set; }

        public double precio { get; set; }

        public double cantidad { get; set; }

    }
}
