using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class MovimientosCripto
    {
        public MovimientosCripto()
        {
            Billeteras = new HashSet<Billetera>();
        }

        public int IdMovimientoCripto { get; set; }
        public string Tipo { get; set; }
        public decimal? Monto { get; set; }
        public int? IdBanco { get; set; }
        public int? IdCriptomoneda { get; set; }

        public virtual Banco IdBancoNavigation { get; set; }
        public virtual Criptomoneda IdCriptomonedaNavigation { get; set; }
        public virtual ICollection<Billetera> Billeteras { get; set; }
    }
}
