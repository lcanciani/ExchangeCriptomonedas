using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int IdFactura { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdTipoMovimiento { get; set; }
        public int? IdBanco { get; set; }

        public virtual BancosUsuario IdBancoNavigation { get; set; }
        public virtual TiposMovimiento IdTipoMovimientoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
