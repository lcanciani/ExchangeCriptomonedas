using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.Request
{
  public class DatosParaExtraccion
  {
    public double saldoUsuario { get; set; }
    public List<BancoUsuarioModel> listBancosUsuario { get; set; }
  }
}
