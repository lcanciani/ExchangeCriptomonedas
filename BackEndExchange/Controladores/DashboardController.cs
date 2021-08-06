using BackEndExchange.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;


namespace BackEndExchange.Controladores
{
  [Route("api/[controller]")]
  [ApiController]
  public class DashboardController : ControllerBase
  {
    private ExchangeDBContext _ex;
    public DashboardController(ExchangeDBContext ex)
    {
      _ex = ex;
    }
    [HttpGet("movimientos/{id}")]
    public IActionResult GetMovimientos(int id)
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        
        var query = from df in _ex.DetalleFacturas
                    join c in _ex.Criptomonedas  on  df.IdCriptomoneda equals c.IdCriptomoneda into cd
                    from subCd in cd.DefaultIfEmpty()
                    join f in _ex.Facturas on df.IdFactura equals f.IdFactura
                    join tm in _ex.TiposMovimientos on f.IdTipoMovimiento equals tm.IdTiposMovimiento
                    where f.IdUsuario == id
                    select new MovimientosUsuarioModel
                    {
                      Fecha =  f.Fecha,
                      Tipo = tm.Tipo.Trim(),
                      Nombre = subCd.Nombre.Trim(),
                      Cantidad =  df.Cantidad,
                      Monto = df.Monto,
                      Precio = df.Precio,
                      IdBanco = f.IdBanco
                    };
        rm.exito = 1;
        rm.data = query.ToList();
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
    }


        [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      RespuestaModel rm = new RespuestaModel();
      try { 
      PrimerDashboardModel pdm = new PrimerDashboardModel();
        var query = from  b in _ex.Billeteras 
                    join c in _ex.Criptomonedas on b.IdCriptomoneda equals c.IdCriptomoneda
                    where b.IdUsuario == id
                    select new PrimerDashboardModel
                    {
                      idUsuario =(int) b.IdUsuario,
                      idBilletera = b.IdBilletera,
                      precioCompra = c.PrecioCompra,
                      cantidad = b.Cantidad,
                      criptomoneda = c.Nombre.Trim(),
                      simbolo = c.Simbolo.Trim()
                  };
        rm.exito = 1;

        rm.data = query.ToList();
        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
    }
      
   
  }
}
