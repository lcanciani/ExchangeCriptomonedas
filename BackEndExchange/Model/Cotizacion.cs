using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Cotizacion
    {
        public int IdCotizacion { get; set; }
        public string Divisa { get; set; }
        public decimal? CotizacionPesos { get; set; }
    }
}
