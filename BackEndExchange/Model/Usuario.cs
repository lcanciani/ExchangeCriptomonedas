using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class Usuario
    {
        public Usuario()
        {
            Billeteras = new HashSet<Billetera>();
            Facturas = new HashSet<Factura>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Dni { get; set; }

        public virtual ICollection<Billetera> Billeteras { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
