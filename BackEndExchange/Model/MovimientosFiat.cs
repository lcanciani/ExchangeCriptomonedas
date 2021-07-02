using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class MovimientosFiat
    {
        public MovimientosFiat()
        {
            Billeteras = new HashSet<Billetera>();
        }

        public int IdMovimientoFiat { get; set; }
        public string Tipo { get; set; }
        public decimal? Monto { get; set; }
        public int? IdBanco { get; set; }
        public int? Cbu { get; set; }
        public decimal? ComisionDepositoRetiro { get; set; }

        public virtual Banco IdBancoNavigation { get; set; }
        public virtual ICollection<Billetera> Billeteras { get; set; }
    }
}
