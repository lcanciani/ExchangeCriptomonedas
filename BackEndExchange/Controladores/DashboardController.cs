using BackEndExchange.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    // GET: api/<ValuesController>
    [HttpGet]
    public IActionResult Get()
    {
      RespuestaModel rm = new RespuestaModel();
      try { 
      PrimerDashboardModel pdm = new PrimerDashboardModel();
        var query = from u in _ex.Usuarios
                    join b in _ex.Billeteras on u.IdUsuario equals b.IdUsuario
                    join c in _ex.Criptomonedas on b.IdCriptomoneda equals c.IdCriptomoneda
                    select new PrimerDashboardModel
                    {
                      idUsuario = u.IdUsuario,
                      nombreUsuario = u.Nombre.Trim(),
                      precioCompra = c.PrecioCompra,
                      cantidad = b.Cantidad,
                      criptomoneda = c.Nombre.Trim(),
                      saldo = u.SaldoFiatUsuario
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
      
    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<ValuesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
