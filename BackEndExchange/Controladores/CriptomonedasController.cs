using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
      RespuestaModel respuesta = new RespuestaModel();
      //devuelve la entidad del contexto, capaz sea mejor pasar los datos a un modelo personalizado
      //y retornar ese objeto
      try {
        var criptos = _ex.Criptomonedas.ToList();
        //var criptos2 = _ex.Criptomonedas.OrderByDescending(d => d.IdCriptomoneda).ToList();
        List<CriptomonedaModel> criptosToFrontend = new List<CriptomonedaModel>();
        foreach (Criptomoneda c in criptos)
        {
          CriptomonedaModel cm = new CriptomonedaModel();
          cm.IdCriptomoneda = c.IdCriptomoneda;
          cm.Nombre = c.Nombre.Trim();
          cm.PrecioCompra = (double)c.PrecioCompra;
          cm.StockDisponible = (double)c.StockDisponible;
          cm.Simbolo = c.Simbolo.Trim();
          cm.StockTotal = (double)c.StockTotal;
          cm.PorcentajeGanancia = (double)c.PorcentajeGanancia;
          cm.ImagenUrl = c.ImagenUrl.Trim();
          cm.FechaBaja = c.FechaBaja;
          criptosToFrontend.Add(cm);
        }
        respuesta.exito = 1;
        respuesta.data = criptosToFrontend;
        return Ok(respuesta);
      }
      catch(Exception e)
      {
        respuesta.exito = 0;
        respuesta.mensanje = e.Message;

        return Ok(respuesta);
      }


      }
      
        // GET api/<CriptomonedasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
      RespuestaModel respuesta = new RespuestaModel();
      try { 
            var c = _ex.Criptomonedas.Find(id);
        CriptomonedaModel cm = new CriptomonedaModel();
        

        cm.IdCriptomoneda = c.IdCriptomoneda;
        cm.Nombre = c.Nombre.Trim();
        cm.PrecioCompra = (double)c.PrecioCompra;
        cm.StockDisponible = (double)c.StockDisponible;
        cm.Simbolo = c.Simbolo.Trim();
        
        cm.StockTotal = (double)c.StockTotal;
        
        cm.ImagenUrl = c.ImagenUrl.Trim();

        respuesta.exito = 1;
        respuesta.data = cm;
        return Ok(respuesta);
            }
            catch (Exception e)
            {
        respuesta.exito = 0;
        respuesta.mensanje = e.Message;
        
                return Ok(respuesta);
            }
        }

        // POST api/<CriptomonedasController>
        [HttpPost]
        public IActionResult Post([FromBody]  CriptomonedaModel model )
        {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        
        Criptomoneda c = new Criptomoneda();
        //c.IdCriptomoneda = model.IdCriptomoneda;
        c.Nombre = model.Nombre;
        c.PrecioCompra = (decimal?)model.PrecioCompra;
        c.StockDisponible = (decimal?)model.StockDisponible;
        c.Simbolo = model.Simbolo;
        
        c.StockTotal = (decimal?)model.StockTotal;
        
        c.ImagenUrl = model.ImagenUrl;
        _ex.Criptomonedas.Add(c);
        _ex.SaveChanges();
        rm.exito = 1;
        rm.mensanje = "funciona";

        return Ok(rm);
      }
      catch (Exception )
      {
        rm.exito = 0;
        rm.mensanje = "no funciona";
        return BadRequest(rm);
      }
    }

        // PUT api/<CriptomonedasController>/5
        [HttpPut]
        public IActionResult Put([FromBody] CriptomonedaModel model)
        {
      RespuestaModel rm = new RespuestaModel();
      try
      {
         
        Criptomoneda c = _ex.Criptomonedas.Find(model.IdCriptomoneda);
        
        c.Nombre = model.Nombre;
        c.PrecioCompra = (decimal?)model.PrecioCompra;
        c.StockDisponible = (decimal?)model.StockDisponible;
        c.Simbolo = model.Simbolo;
        c.PorcentajeGanancia = (decimal?)model.PorcentajeGanancia;
        c.StockTotal = (decimal?)model.StockTotal;       
        c.ImagenUrl = model.ImagenUrl;
        c.FechaBaja = model.FechaBaja;
        _ex.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _ex.SaveChanges();
        
        rm.exito = 1;
        rm.data = _ex.Criptomonedas.ToList();
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
      
        }

        // DELETE api/<CriptomonedasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
      RespuestaModel rm = new RespuestaModel();
            try
            {
        var c = _ex.Criptomonedas.Find(id);
        if(c.FechaBaja != null)
        {
          rm.exito = 0;
          rm.mensanje = "La criptomoneda ya esta dada de baja";
          rm.data = _ex.Criptomonedas.ToList();
          return Ok(rm);
        }
        c.FechaBaja = DateTime.Now;
        _ex.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _ex.SaveChanges();
        rm.exito = 1;
        rm.data = _ex.Criptomonedas.ToList();
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
