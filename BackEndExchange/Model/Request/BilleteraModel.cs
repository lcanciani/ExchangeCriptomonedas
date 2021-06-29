using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class BilleteraModel
    {
        public int IdBilletera { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Cantidad { get; set; }
        public int? IdCriptomoneda { get; set; }
        public int? IdMovimientoFiat { get; set; }
        public int? IdMovimientoCripto { get; set; }
        public int? IdUsuario { get; set; }
        public string DireccionBilletera { get; set; }
    }
}
