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
        }

        public int IdCriptomoneda { get; set; }
        public string Nombre { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? StockDisponible { get; set; }
        public string Simbolo { get; set; }
        public decimal? StockTotal { get; set; }
        public decimal? PorcentajeGanancia { get; set; }
        public string ImagenUrl { get; set; }
        public DateTime? FechaBaja { get; set; }

        public virtual ICollection<Billetera> Billeteras { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
