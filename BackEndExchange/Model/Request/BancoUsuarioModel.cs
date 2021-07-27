using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class BancoUsuarioModel
  {
    public int IdBancoUsuario { get; set; }
    public int? IdBanco { get; set; }
    public int? IdUsuario { get; set; }
    public string Cbu { get; set; }
    public string RazonSocial { get; set; }
  }
}
