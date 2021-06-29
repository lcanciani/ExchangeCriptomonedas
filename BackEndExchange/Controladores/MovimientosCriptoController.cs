using BackEndExchange.Model;
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

    public class MovimientosCriptoController : ControllerBase
    {

        private ExchangeDBContext _ex;

        public MovimientosCriptoController(ExchangeDBContext ex)
        {
            _ex = ex;
        }

        // GET: api/<MovimientosCriptoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
               var movimientos = _ex.MovimientosCriptos.ToList();
                return Ok(movimientos);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // GET api/<MovimientosCriptoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovimientosCriptoController>
        [HttpPost]
        public IActionResult Post([FromBody] MovimientoCriptoModel model)
        {
            try
            {
                MovimientosCripto mc = new MovimientosCripto();
                mc.IdMovimientoCripto = model.IdMovimientoCripto;

                _ex.MovimientosCriptos.Add(mc);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<MovimientosCriptoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovimientosCriptoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                MovimientosCripto mc = new MovimientosCripto();
                mc = _ex.MovimientosCriptos.Find(id);
                if (mc == null)
                    return BadRequest("La billetera que intenta eliminar no existe");

                _ex.MovimientosCriptos.Remove(mc);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
