using BackEndExchange.Model;
using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
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
    public class BancosController : ControllerBase
    {
        private ExchangeDBContext _ex;
        public BancosController(ExchangeDBContext ex)
        {
            _ex = ex;

        }

        // GET: api/<BancosController>
        [HttpGet]
        public IActionResult Get()
        {
            try {
                List<BancoModel> banco = new List<BancoModel>(); ; 
            var bancos = _ex.Bancos.ToList();

            foreach(var ban in bancos)
                {
                    BancoModel bm = new BancoModel();
                    bm.IdBanco = ban.IdBanco;
                    bm.RazonSocial = ban.RazonSocial;
                    bm.Direccion = ban.Direccion;
                    bm.Ciudad = ban.Ciudad;
                    bm.Ciut = ban.Ciut;
                    bm.Telefono = ban.Telefono;
                    banco.Add(bm);
                }
            return Ok(banco);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            }


        // GET api/<BancosController>/5
            [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var bancos = _ex.Bancos.Find(id);
                return Ok(bancos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<BancosController>
        [HttpPost]
        
        public IActionResult Post([FromBody] BancoModel model)
        {
            try
            {
                Banco b = new Banco();
                b.IdBanco = model.IdBanco;
                b.RazonSocial = model.RazonSocial;
                b.Direccion = model.Direccion;
                b.Ciudad = model.Ciudad;
                b.Ciut = model.Ciut;
                b.Telefono = model.Telefono;

                _ex.Bancos.Add(b);
                _ex.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<BancosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BancoModel model)
        {
            Respuesta resp = new Respuesta();
            try
            {
                
                var bancoModificar = _ex.Bancos.Find(id);
                if (bancoModificar == null)
                {
                    resp.exito = 0;
                    resp.mensaje = "El banco a modificar no existe";
                    return BadRequest(resp);
                }
                    

                bancoModificar.RazonSocial = model.RazonSocial;
                bancoModificar.Direccion = model.Direccion;
                bancoModificar.Ciudad = model.Ciudad;
                bancoModificar.Ciut = model.Ciut;
                bancoModificar.Telefono = model.Telefono;
                _ex.Bancos.Update(bancoModificar);
                _ex.SaveChanges();
                resp.exito = 1;
                resp.mensaje = "El banco se modifico correctamente";
                return Ok(resp);
            }
            catch(Exception e)
            {
                resp.exito = 0;
                resp.mensaje = "No se pudo modificar el registro: exception "+ e.Message;
                
                
                return BadRequest(resp);
            }

        }

        // DELETE api/<BancosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                Banco b = new Banco();
                b = _ex.Bancos.Find(id);
                if (b == null)
                    return BadRequest("El Banco que intenta eliminar no existe");

                _ex.Bancos.Remove(b);
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
