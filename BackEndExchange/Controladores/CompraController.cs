using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndExchange.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BackEndExchange.Services;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndExchange.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private ExchangeDBContext _ex;
        private ICompraService _cs;
         public CompraController (ExchangeDBContext ex, ICompraService cs)
    {
      _ex = ex;
      _cs = cs;
    }

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
        public IActionResult Get(int id)
        {

      RespuestaModel respuesta = new RespuestaModel();
      try
      {
        
        var c = _ex.Criptomonedas.Find(id);
        var movimiento = _ex.TiposMovimientos.FirstOrDefault(mov => mov.Tipo == "CompraCripto");

        CriptomonedaModel cm = new CriptomonedaModel();
        cm.IdCriptomoneda = c.IdCriptomoneda;
        cm.Nombre = c.Nombre.Trim();
        cm.PrecioCompra = (decimal)c.PrecioCompra;
        cm.StockDisponible = (decimal)c.StockDisponible;
        cm.Simbolo = c.Simbolo.Trim();
        cm.StockTotal = (decimal)c.StockTotal;
        cm.ImagenUrl = c.ImagenUrl.Trim();
        cm.PorcentajeGanancia = (decimal)c.PorcentajeGanancia;

        TipoMovimientoModel tmm = new TipoMovimientoModel();
        tmm.IdTiposMovimiento = movimiento.IdTiposMovimiento;
        tmm.Tipo = movimiento.Tipo.Trim();
        tmm.Comision = (double)movimiento.Comision;

        

        ConfiguracionCompraModel ccm = new ConfiguracionCompraModel();
        ccm.IdCriptomoneda = cm.IdCriptomoneda;
        ccm.NombreCriptomoneda = cm.Nombre;
        ccm.PrecioVenta = getPrecioVentaPesos((double)cm.PrecioCompra, (double)cm.PorcentajeGanancia);
        ccm.Comision = tmm.Comision;
        ccm.IdTipoMovimiento = tmm.IdTiposMovimiento;
        
        respuesta.exito = 1;
        respuesta.data = ccm;

        return Ok(respuesta);
      }
      catch (Exception e)
      {
        respuesta.exito = 0;
        respuesta.mensanje = e.Message;

        return Ok(respuesta);
      }
    }

        // POST api/<CompraController>
        [HttpPost]
        public IActionResult Post([FromBody] ConfirmarCompraModel model)
        {

      
      return Ok(_cs.add(model));
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





    }

        // PUT api/<CompraController>/5
       

        // DELETE api/<CompraController>/5
       

    private double getPrecioVentaPesos( double precioCompra, double porcentajeGanancia)
    {
      double precioVenta = precioCompra * (porcentajeGanancia  + 1);
      var cotizacion = _ex.Cotizacions.Find(1);
      precioVenta = precioVenta * (double)cotizacion.CotizacionPesos;
      return precioVenta;
    }
        
    }
}
