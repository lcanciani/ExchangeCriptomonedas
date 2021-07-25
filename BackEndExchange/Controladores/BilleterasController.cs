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
    public class BilleterasController : ControllerBase
    {
        private ExchangeDBContext _ex;

        public BilleterasController(ExchangeDBContext ex)
        {
            _ex = ex;
        }
        
        // GET: api/<BilleterasController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<BilleteraModel> billeterasList = new List<BilleteraModel>(); ;
                var billeteras = _ex.Billeteras.ToList();

                foreach (var bil in billeteras)
                {
                    BilleteraModel bm = new BilleteraModel();
                    bm.IdBilletera = bil.IdBilletera;
                    billeterasList.Add(bm);
                }
                _ex.SaveChanges();
                return Ok(billeterasList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET api/<BilleterasController>/5
        [HttpGet("{id}")]
        public void Get(int id)
        {
      
    }

        // POST api/<BilleterasController>
        [HttpPost]
        public IActionResult Post([FromBody] BilleteraModel model)
        {
            try
            {
                Billetera b = new Billetera();
                b.IdBilletera = model.IdBilletera;

                _ex.Billeteras.Add(b);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<BilleterasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BilleterasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                
                Billetera c = new Billetera();
                c = _ex.Billeteras.Find(id);
                if (c == null) 
                    return BadRequest("La billetera que intenta eliminar no existe");

                _ex.Billeteras.Remove(c);
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
