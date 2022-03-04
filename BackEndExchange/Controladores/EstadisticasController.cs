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
  public class EstadisticasController : ControllerBase
  {

    private ExchangeDBContext _ex;

    public EstadisticasController(ExchangeDBContext ex)
    {
      _ex = ex;
    }
    // GET: api/<EstadisticasController>
    [HttpGet]
    
    public IActionResult Get()
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        
        rm.exito = 1;

        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;

        return Ok(rm);
      }
    }

    // GET api/<EstadisticasController>/5
    [HttpGet("criptomonedaEstadisticas")]
    public IActionResult criptomonedaEstadisticas()
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        var query = from c in _ex.Criptomonedas
                    join df in _ex.DetalleFacturas on c.IdCriptomoneda equals df.IdCriptomoneda
                    join f in _ex.Facturas on df.IdFactura equals f.IdFactura
                    select new {c.IdCriptomoneda, c.Nombre} into c
                    group c by new { c.IdCriptomoneda, c.Nombre } into g
                    
                    select new CriptoConsAgrupModel
                    {
                      IdCriptomoneda = g.Key.IdCriptomoneda,
                      nombreCripto = g.Key.Nombre.Trim(),
                      CantidadMovimientos = g.Select(x => x.IdCriptomoneda).Count()
                    };
        query.ToList();
        rm.data = query;
        rm.exito = 1;
        rm.mensanje = "Consulta realizada correctamente";
        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;

        return Ok(rm);
      }
    }
    //devuelve los datos de
    [HttpPost("reporteCompras")]

    public IActionResult reporteCompras([FromBody] FechasReporteComprasModel rvm)
    {
      RespuestaModel rm = new RespuestaModel();
      DateTime desde = DateTime.Today;
      DateTime hasta = DateTime.Today.AddDays(1);
      int temporalidad = rvm.tipo;
      switch (temporalidad)
      {
        
          //esta semana
        case 1:
          desde = DateTime.Today.AddDays(-7);
          break;
        //lo que va del mes
        case 2:
          desde = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
          break;
        //ultimos 30 dias
        case 3:
          desde = DateTime.Today.AddDays(-30);
          
          break;
        //personalizado
        case 4:
          desde = rvm.fechaInicio;
          hasta = rvm.fechaFin.AddDays(1);
          break;
      }


      try
      {

        /*select f.idUsuario
  from Facturas f 
  join DetalleFactura df on f.idFactura = df.idFactura
  where f.fecha between '01/23/2022' and '01/25/2022'
order by f.idUsuario*/
        var query = from f in _ex.Facturas
                    join df in _ex.DetalleFacturas on f.IdFactura equals df.IdFactura
                    join u in _ex.Usuarios on f.IdUsuario equals u.IdUsuario
                    where f.Fecha >= desde && f.Fecha <= hasta
                    select new { f.IdUsuario, u.Apellido, u.Nombre, df.Cantidad, df.Precio } into s
                    group s by new { s.IdUsuario, s.Apellido, s.Nombre } into g

                    select new ReporteComprasModel
                    {
                      idUsuario = g.Key.IdUsuario,
                      apellido = g.Key.Apellido.Trim(),
                      nombre = g.Key.Nombre.Trim(),
                      montoTotal = g.Sum(x => x.Cantidad * x.Precio)
                    };
        query.ToList();
        rm.data = query;
        rm.exito = 1;
        rm.mensanje = "Consulta realizada correctamente";
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;

        return Ok(rm);
      }
      
    }
    // POST api/<EstadisticasController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<EstadisticasController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<EstadisticasController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
