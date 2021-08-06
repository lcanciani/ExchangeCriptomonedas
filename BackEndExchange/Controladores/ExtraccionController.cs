using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
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
  public class ExtraccionController : ControllerBase
  {
    private ExchangeDBContext _ex;

    public ExtraccionController(ExchangeDBContext ex)
    {
      _ex = ex;
    }
    


    [HttpPost("/datosExtraccion")]
    public IActionResult datosExtraccion([FromBody] int idUsuario)
    {
      RespuestaModel rm = new RespuestaModel();
      DatosParaExtraccion datosExtraccion = new DatosParaExtraccion();
      try
      {
        List<BancoUsuarioModel> listBancoUsuarioModel = new List<BancoUsuarioModel>();
        var usuario = _ex.Usuarios.Find(idUsuario);
        double saldoFiat;
        saldoFiat =(double)usuario.SaldoFiatUsuario;
       // var bancosUsuario = _ex.BancosUsuarios.Where(bu => bu.IdUsuario == idUsuario);
       var bancosUsuario = from bu in _ex.BancosUsuarios
                           join b in _ex.Bancos on bu.IdBanco equals b.IdBanco
                           where bu.IdUsuario == idUsuario
                           select new BancoUsuarioModel
                           {
                             IdBancoUsuario = bu.IdBancoUsuario,
                             IdBanco = bu.IdBanco,
                             RazonSocial = b.RazonSocial,
                             Cbu = bu.Cbu,
                             IdUsuario = bu.IdUsuario                             
                           };
        foreach (BancoUsuarioModel bancoUsuario in bancosUsuario)
        {
          bancoUsuario.RazonSocial = bancoUsuario.RazonSocial.Trim();
          bancoUsuario.Cbu = bancoUsuario.Cbu.Trim();
          listBancoUsuarioModel.Add(bancoUsuario);
        }
        
        datosExtraccion.saldoUsuario = saldoFiat;
        datosExtraccion.listBancosUsuario = listBancoUsuarioModel;
        rm.exito = 1;
        rm.data = datosExtraccion;
        return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok(rm);
      }
      
    }

   
    // POST api/<ExtraccionController>
    [HttpPost("/registrarExtraccion")]
    public IActionResult registrarExtraccion([FromBody] ExtraccionModel model)
    {
      RespuestaModel rm = new RespuestaModel();
      try {
        
        var usuario = _ex.Usuarios.Where(u => u.IdUsuario == model.IdUsuario).FirstOrDefault();
        usuario.SaldoFiatUsuario -= (decimal)model.Monto;
        _ex.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        Factura f = new Factura();
        f.Fecha = DateTime.Now;
        f.IdBanco = model.IdBanco;
        f.IdTipoMovimiento = model.IdTipoMovimiento;
        f.IdUsuario = model.IdUsuario;
        _ex.Facturas.Add(f);
        _ex.SaveChanges();
        DetalleFactura df = new DetalleFactura();
        df.IdFactura = f.IdFactura;
        df.IdCriptomoneda = null;
        df.CotizacionDolar = null;
        df.Comision = (decimal) model.Comision;
        df.PorcentajeGanancia = null;
        df.Monto = (decimal)model.Monto;
        df.Precio = null;
        df.Cantidad = null;
        _ex.DetalleFacturas.Add(df);

        _ex.SaveChanges();
        rm.exito = 1;

      return Ok(rm);
      }
      catch(Exception e)
      {
        rm.exito = 0;
        rm.mensanje = e.Message;
        return Ok();
      }
    }
  }
}
