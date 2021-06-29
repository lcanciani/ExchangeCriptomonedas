using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Criptomoneda
    {
        public Criptomoneda()
        {
            Billeteras = new HashSet<Billetera>();
            DetalleFacturas = new HashSet<DetalleFactura>();
            MovimientosCriptos = new HashSet<MovimientosCripto>();
        }

        public int IdCriptomoneda { get; set; }
        public string Nombre { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Stock { get; set; }
        public string Simbolo { get; set; }
        public decimal? Capitalizacion { get; set; }
        public decimal? ValorTotal { get; set; }

        public virtual ICollection<Billetera> Billeteras { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual ICollection<MovimientosCripto> MovimientosCriptos { get; set; }
    }
}
