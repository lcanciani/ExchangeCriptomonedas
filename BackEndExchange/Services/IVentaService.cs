using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Services
{
  public interface IVentaService
  {
    public RespuestaModel add(ConfirmarVentaModel model);
  }
}
