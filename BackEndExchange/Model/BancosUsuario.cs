using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class BancosUsuario
    {
        public BancosUsuario()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdBancoUsuario { get; set; }
        public int? IdBanco { get; set; }
        public int? IdUsuario { get; set; }
        public string Cbu { get; set; }

        public virtual Banco IdBancoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
