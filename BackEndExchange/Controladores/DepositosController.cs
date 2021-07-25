using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model;
using BackEndExchange.Model.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndExchange.Controladores
{
  [Route("api/[controller]")]
  [ApiController]
  public class DepositosController : ControllerBase
  {
    // GET: api/<DepositosController>


    // GET api/<DepositosController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<DepositosController>
    [HttpPost]
    public IActionResult Post([FromBody] DepositoModel model)
    {
      RespuestaModel rm = new RespuestaModel();
      using (var exchangeDb = new ExchangeDBContext())
      {



        using (var registrarDeposito = exchangeDb.Database.BeginTransaction())
        {
          try
          {
            var usuario = exchangeDb.Usuarios.Where(u => u.IdUsuario == model.IdUsuario).FirstOrDefault();

            usuario.SaldoFiatUsuario +=  model.Monto - (model.Monto * model.comision);
            exchangeDb.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            exchangeDb.SaveChanges();

            Factura f = new Factura();
            f.IdBanco = model.IdBanco;
            f.IdTipoMovimiento = model.IdTipoMovimiento;
            f.Fecha = DateTime.Now;
            f.IdUsuario = model.IdUsuario;
            exchangeDb.Facturas.Add(f);
            exchangeDb.SaveChanges();

            DetalleFactura df = new DetalleFactura();
            df.IdFactura = f.IdFactura;
            //var tipoMovimiento = exchangeDb.TiposMovimientos.Find(model.IdTipoMovimiento);
            df.Comision = model.comision;
            df.Monto = model.Monto;
            exchangeDb.DetalleFacturas.Add(df);

            exchangeDb.SaveChanges();
            rm.exito = 1;
            registrarDeposito.Commit();
            return Ok(rm);
          }
          catch (Exception e)
          {
            rm.exito = 0;
            rm.mensanje = e.Message;
            registrarDeposito.Rollback();
            return Ok(rm);
          }

        }
      }
    }
      // PUT api/<DepositosController>/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody] string value)
      {
      }

      // DELETE api/<DepositosController>/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }
    }
  }
