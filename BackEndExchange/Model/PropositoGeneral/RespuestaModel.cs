using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Model.PropositoGeneral
{
  public class RespuestaModel
  {
    public int exito { get; set; }
    public string mensanje { get; set; }
    public object data { get; set; }
  }
}
