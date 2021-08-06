using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndExchange.Controladores
{
  [Route("api/[controller]")]
  [ApiController]
  public class EstadisticasController : ControllerBase
  {

    private ExchangeDBContext _ex;

    public EstadisticasController(ExchangeDBContext ex)
    {
      _ex = ex;
    }
    // GET: api/<EstadisticasController>
    [HttpGet]
    
    public IActionResult Get()
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        
        rm.exito = 1;

        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;

        return Ok(rm);
      }
    }

    // GET api/<EstadisticasController>/5
    [HttpGet("criptomonedaEstadisticas")]
    public IActionResult criptomonedaEstadisticas()
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        var query = from c in _ex.Criptomonedas
                    join df in _ex.DetalleFacturas on c.IdCriptomoneda equals df.IdCriptomoneda
                    join f in _ex.Facturas on df.IdFactura equals f.IdFactura
                    select new {c.IdCriptomoneda, c.Nombre} into c
                    group c by new { c.IdCriptomoneda, c.Nombre } into g
                    
                    select new CriptoConsAgrupModel
                    {
                      IdCriptomoneda = g.Key.IdCriptomoneda,
                      nombreCripto = g.Key.Nombre.Trim(),
                      CantidadMovimientos = g.Select(x => x.IdCriptomoneda).Count()
                    };
        query.ToList();
        rm.data = query;
        rm.exito = 1;
        rm.mensanje = "Consulta realizada correctamente";
        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;

        return Ok(rm);
      }
    }

    // POST api/<EstadisticasController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<EstadisticasController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<EstadisticasController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
