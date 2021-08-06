using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class RegistrarUsuarioModel
  {
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Dni { get; set; }
    public  BancoCbu[] BancoCbu { get; set; }
  }
}
