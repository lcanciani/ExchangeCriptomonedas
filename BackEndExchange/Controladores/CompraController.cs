using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndExchange.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndExchange.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private ExchangeDBContext exchangeDB;
        
       

        // GET: api/<CompraController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                
                return Ok();
            }

            catch (Exception e)
            {
                return  BadRequest(e.Message);
            }
            
        }



        // GET api/<CompraController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CompraController>
        [HttpPost]
        public IActionResult Post([FromBody] Model.Request.FacturaModel model)
        {
            

            
           //using(var exchangeDb = new ExchangeDBContext())
           // {


           //         using (var registrarCompra = exchangeDb.Database.BeginTransaction())
           //         {
           //             try
           //             {
                            
           //                 Factura facturaModel = new Factura();
           //                 facturaModel.IdUsuario = model.idUsuario;
           //                 facturaModel.Fecha = DateTime.Now;
           //                 exchangeDb.Facturas.Add(facturaModel);
           //                 exchangeDb.SaveChanges();

           //                 foreach (var detalle in model.detalleFactura)
           //                 {
                                
           //                     var dt = new DetalleFactura();
           //                     dt.IdCriptomoneda = detalle.idCriptomoneda;
           //                     dt.Precio =(decimal) detalle.precio;
           //                     dt.Cantidad =(decimal) detalle.cantidad;
           //                     dt.IdFactura = facturaModel.IdFactura;
           //                     exchangeDb.DetalleFacturas.Add(dt);
           //                     exchangeDb.SaveChanges();
           //                 }
                            
           //                 registrarCompra.Commit();
           //                 return Ok();
           //             }

           //             catch (Exception e)
           //             {

           //                 registrarCompra.Rollback();
           //                 return BadRequest(e.Message);
           //             }

           //         }
                
           // }

           
                return BadRequest();
            

        }

        // PUT api/<CompraController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompraController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private void spRegistrarfactura(int value)
        {
            exchangeDB = new ExchangeDBContext();
            DateTime fecha = DateTime.Now;
            int idFactura = 0;
            var sqlParameter1 = new SqlParameter("@idUsuario", value);
            var sqlParameterFecha = new SqlParameter("@fecha", fecha);
            var sqlParameterSalida = new SqlParameter("@idFactura", idFactura);
            //exchangeDB.FacturaId.FromSqlRaw(" exec RegistrarFactura @fecha, @idUsuario, @idFactura out", sqlParameter1, sqlParameterFecha, sqlParameterSalida).ToList();
            
        }
    }
}
