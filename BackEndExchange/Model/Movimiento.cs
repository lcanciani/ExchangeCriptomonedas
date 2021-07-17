using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Movimiento
    {
        public Movimiento()
        {
            Billeteras = new HashSet<Billetera>();
        }

        public int IdMovimiento { get; set; }
        public string Tipo { get; set; }
        public decimal? Monto { get; set; }
        public int? IdBanco { get; set; }
        public int? IdCriptomoneda { get; set; }
        public int? IdTiposMovimiento { get; set; }
        public decimal? Cantidad { get; set; }

        public virtual Banco IdBancoNavigation { get; set; }
        public virtual Criptomoneda IdCriptomonedaNavigation { get; set; }
        
        public virtual ICollection<Billetera> Billeteras { get; set; }
    }
}
