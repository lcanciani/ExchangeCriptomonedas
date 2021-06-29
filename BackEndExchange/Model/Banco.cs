using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Banco
    {
        public Banco()
        {
            MovimientosCriptos = new HashSet<MovimientosCripto>();
            MovimientosFiats = new HashSet<MovimientosFiat>();
        }

        public int IdBanco { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Ciut { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<MovimientosCripto> MovimientosCriptos { get; set; }
        public virtual ICollection<MovimientosFiat> MovimientosFiats { get; set; }
    }
}
