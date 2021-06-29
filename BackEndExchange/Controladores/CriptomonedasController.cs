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
    public class CriptomonedasController : ControllerBase
    {
        private ExchangeDBContext _ex;
        public CriptomonedasController(ExchangeDBContext ex)
        {
            _ex = ex;
        }

        // GET: api/<CriptomonedasController>
        [HttpGet]
        public IActionResult Get()
        {

            //devuelve la entidad del contexto, capaz sea mejor pasar los datos a un modelo personalizado
            //y retornar ese objeto
               var criptos = _ex.Criptomonedas.ToArray();
                return Ok(criptos);
            
            
        }

        // GET api/<CriptomonedasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try { 
            var criptos = _ex.Criptomonedas.Find(id);
            return Ok(criptos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<CriptomonedasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<CriptomonedasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CriptomonedasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(CriptomonedaModel model)
        {
            try
            {
                Criptomoneda c = new Criptomoneda();
                c.IdCriptomoneda = 1;
                _ex.Criptomonedas.Remove(c);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
