using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
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
  public class TipoMovimientoController : ControllerBase
  {
    private ExchangeDBContext _ex;
    public TipoMovimientoController(ExchangeDBContext ex)
    {
      _ex = ex;
    }
    // GET: api/<ComisionController>
    [HttpGet]
    public IActionResult Get()
    {
      return Ok();
    }

    // GET api/<ComisionController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        var tipoMovimiento = _ex.TiposMovimientos.Where(d => d.IdTiposMovimiento == id).FirstOrDefault();
        
        if(tipoMovimiento == null)
        {
          rm.exito = 0;
          rm.mensanje = "el tipo de movimiento no existe";
          return Ok(rm);
        }
        TipoMovimientoModel tmm = new TipoMovimientoModel();
        tmm.IdTiposMovimiento = tipoMovimiento.IdTiposMovimiento;
        tmm.Tipo = tipoMovimiento.Tipo.Trim();
        tmm.Comision = (double)tipoMovimiento.Comision;

        rm.exito = 1;
        rm.data = tmm;
        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
    }

    // POST api/<ComisionController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ComisionController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ComisionController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
