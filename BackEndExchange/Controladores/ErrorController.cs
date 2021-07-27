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
  public class ErrorController : ControllerBase
  {
    private ExchangeDBContext _ex;

    public ErrorController (ExchangeDBContext ex)
    {
      _ex = ex;
    }
    // GET: api/<ErrorController>
    [HttpGet]
    public IActionResult Get()
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        var error = _ex.Errors.FirstOrDefault();
        
        rm.exito = 1;
        rm.data = error;
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
    }

    // GET api/<ErrorController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<ErrorController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ErrorController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ErrorController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
