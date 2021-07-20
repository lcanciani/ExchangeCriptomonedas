using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Billetera
    {
        public int IdBilletera { get; set; }
        public decimal? Cantidad { get; set; }
        public int? IdUsuario { get; set; }
        public string DireccionBilletera { get; set; }
        public decimal? ClavePrivada { get; set; }
        public decimal? ClavePublica { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int? IdCriptomoneda { get; set; }

        public virtual Criptomoneda IdCriptomonedaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
