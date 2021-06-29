using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class MovimientoCriptoModel
    {
        public int IdMovimientoCripto { get; set; }
        public string Tipo { get; set; }
        public decimal? Monto { get; set; }
        public int? IdBanco { get; set; }
        public int? IdCriptomoneda { get; set; }
    }
}
