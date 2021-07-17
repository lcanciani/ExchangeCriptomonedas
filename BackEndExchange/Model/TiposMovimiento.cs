using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class TiposMovimiento
    {
        public TiposMovimiento()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdTiposMovimiento { get; set; }
        public string Tipo { get; set; }
        public decimal? Comision { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
