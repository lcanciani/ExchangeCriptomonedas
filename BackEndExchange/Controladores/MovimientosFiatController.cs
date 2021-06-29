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
    public class MovimientosFiatController : ControllerBase
    {

        private ExchangeDBContext _ex;

        public MovimientosFiatController(ExchangeDBContext ex)
        {
            _ex = ex;
        }
        // GET: api/<MovimientosFiatController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var movimientos = _ex.MovimientosFiats.ToList();
                return Ok(movimientos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET api/<MovimientosFiatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovimientosFiatController>
        [HttpPost]
        public IActionResult Post([FromBody] MovimientoFiatModel model)
        {
            try
            {
                MovimientosFiat mf = new MovimientosFiat();
                mf.IdMovimientoFiat = model.IdMovimientoFiat;

                _ex.MovimientosFiats.Add(mf);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<MovimientosFiatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovimientosFiatController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                MovimientosFiat mf = new MovimientosFiat();
                mf = _ex.MovimientosFiats.Find(id);
                if (mf == null)
                    return BadRequest("El Movimiento que intenta eliminar no existe");

                _ex.MovimientosFiats.Remove(mf);
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
