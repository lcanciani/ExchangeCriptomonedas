using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class FacturaModel
    {

        


        public int Id { get; set; }
        public int idUsuario { set; get; }
        public DateTime fecha { get; set; }
        public List<DetalleFacturaModel> detalleFactura { get; set; }


        public FacturaModel()
        {
            detalleFactura = new List<DetalleFacturaModel>();
        }
    }
}
