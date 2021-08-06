using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
    public class UsuarioModel
    {
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }
    public string Email { get; set; }
    public string Contrasenia { get; set; }
    public DateTime? FechaBaja { get; set; }
    public string Dni { get; set; }
    public decimal? SaldoFiatUsuario { get; set; }
  }
}
