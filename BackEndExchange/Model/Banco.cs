using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Banco
    {
        public Banco()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdBanco { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Ciut { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaBaja { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
