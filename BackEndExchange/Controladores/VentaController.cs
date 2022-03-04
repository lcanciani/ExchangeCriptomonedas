using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using BackEndExchange.Services;
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
  public class VentaController : ControllerBase
  {
    private ExchangeDBContext _ex;
    private IVentaService _vs;
    public VentaController (ExchangeDBContext ex, IVentaService vs)
    {
      _vs = vs;
      _ex = ex;
    }
    // GET: api/<VentaController>
    [HttpGet]
    public IActionResult Get()
    {
      return Ok();
    }

    // GET api/<VentaController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      RespuestaModel respuesta = new RespuestaModel();
      try
      {

        var c = _ex.Criptomonedas.Find(id);
        var movimiento = _ex.TiposMovimientos.Where(mov => mov.Tipo == "VentaCripto").FirstOrDefault();

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

        var cotizacion = _ex.Cotizacions.FirstOrDefault();


        ConfirmarVentaModel ccm = new ConfirmarVentaModel();
        ccm.IdCriptomoneda = cm.IdCriptomoneda;
        ccm.NombreCriptomoneda = cm.Nombre;
        //ccm.PrecioVenta = getPrecioVentaPesos(cm.PrecioCompra, cm.PorcentajeGanancia);
        ccm.PrecioCompra = (double)cm.PrecioCompra;
        ccm.Comision = tmm.Comision;
        ccm.IdTipoMovimiento = tmm.IdTiposMovimiento;
        ccm.CotizacionDolar = (double)cotizacion.CotizacionPesos;
        Console.WriteLine("AAAAAAAAAACOtiza: " + (double)cotizacion.CotizacionPesos);
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

    // POST api/<VentaController>
    [HttpPost]
    public IActionResult Post([FromBody] ConfirmarVentaModel model)
    {
      return Ok(_vs.add(model));

    }

    // PUT api/<VentaController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<VentaController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
