using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Billetera
    {
        public int IdBilletera { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Cantidad { get; set; }
        public int? IdCriptomoneda { get; set; }
        public int? IdMovimientoFiat { get; set; }
        public int? IdMovimientoCripto { get; set; }
        public int? IdUsuario { get; set; }
        public string DireccionBilletera { get; set; }

        public virtual Criptomoneda IdCriptomonedaNavigation { get; set; }
        public virtual MovimientosCripto IdMovimientoCriptoNavigation { get; set; }
        public virtual MovimientosFiat IdMovimientoFiatNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
