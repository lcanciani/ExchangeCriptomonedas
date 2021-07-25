using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using BackEndExchange.Model.Response;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Services
{
  public class CompraService : ICompraService
  {


    public RespuestaModel add(ConfirmarCompraModel model)
    {

      RespuestaModel rm = new RespuestaModel();
      using (var exchangeDb = new ExchangeDBContext())
      {
        double totalFactura = model.PrecioVenta + model.PrecioVenta * (model.Comision + 1);


        using (var registrarCompra = exchangeDb.Database.BeginTransaction())
        {
          try
          {

            var usuario = exchangeDb.Usuarios.Where(d => d.IdUsuario == model.IdUsuario).FirstOrDefault();
            var Billetera = new Billetera();
            Billetera.Cantidad = 0;
            if (totalFactura <= (double)usuario.SaldoFiatUsuario)
            {

              usuario.SaldoFiatUsuario = usuario.SaldoFiatUsuario - (decimal)totalFactura;
              exchangeDb.Entry<Usuario>(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
              Console.WriteLine("AAAAAAAAAA0");
              exchangeDb.SaveChanges();
              double precioCompra = 0;

              Factura fm = new Factura();
              var dfm = new DetalleFactura();
              fm.IdUsuario = model.IdUsuario;
              fm.Fecha = DateTime.Now;
              fm.IdTipoMovimiento = model.IdTipoMovimiento;
              fm.IdBanco = null;
              exchangeDb.Facturas.Add(fm);
              Console.WriteLine("AAAAAAAAAA1");
              exchangeDb.SaveChanges();

              dfm.IdCriptomoneda = model.IdCriptomoneda;
              dfm.IdFactura = fm.IdFactura;
              precioCompra = model.PrecioVenta / (model.PorcentajeGanancia + 1);
              dfm.Cantidad = (decimal?)model.Cantidad;
              dfm.Precio = (decimal)precioCompra;

              var cotizacionDolar = exchangeDb.Cotizacions.Single(d => d.IdCotizacion == 1);
              dfm.CotizacionDolar = cotizacionDolar.CotizacionPesos;
              dfm.Comision = (decimal?)model.Comision;
              dfm.PorcentajeGanancia = (decimal?)model.PorcentajeGanancia;
              exchangeDb.DetalleFacturas.Add(dfm);
              Console.WriteLine("AAAAAAAAAA2");
              exchangeDb.SaveChanges();

             
              //si billetera es null entonce el usuario no tiene  esa criptomoneda, hay q agrgarla,
              //sino hay que editarla.
              var billetera = exchangeDb.Billeteras.Where(d =>

                d.IdUsuario == model.IdUsuario && d.IdCriptomoneda == model.IdCriptomoneda
              ).FirstOrDefault();
              Console.WriteLine("AAAAAAAAAA2: "+ billetera);
              if (billetera != null)
              {
                Billetera b2 = new Billetera();
                billetera.Cantidad = billetera.Cantidad + (decimal?)model.Cantidad;

                billetera.IdUsuario = model.IdUsuario;
                billetera.IdCriptomoneda = model.IdCriptomoneda;
                billetera.FechaBaja = null;
                billetera.DireccionBilletera = null;
                billetera.ClavePrivada = null;
                billetera.ClavePublica = null;
                //exchangeDb.Billeteras.Update(Billetera);
                exchangeDb.Entry(billetera).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                exchangeDb.SaveChanges();
                Console.WriteLine("AAAAAAAAAA4");
              }

              else { 
              



                Billetera.Cantidad = (decimal?)model.Cantidad;
                Billetera.IdUsuario = model.IdUsuario;
                Billetera.IdCriptomoneda = model.IdCriptomoneda;
                Billetera.FechaBaja = null;
                Billetera.DireccionBilletera = null;
                Billetera.ClavePrivada = null;
                Billetera.ClavePublica = null;
                exchangeDb.Billeteras.Add(Billetera);
                Console.WriteLine("AAAAAAAAAA3");
                exchangeDb.SaveChanges();
              }
              exchangeDb.SaveChanges();
              registrarCompra.Commit();

              rm.exito = 1;
              return rm;
            }


            else
            {
              rm.exito = 0;
              rm.mensanje = "El saldo es insuficiente";
              return rm;
            }
          }
          catch (Exception e)
          {
            Console.WriteLine(e.Message);
            registrarCompra.Rollback();
            rm.mensanje = "fallo try " + e.Message;
            rm.exito = 0;
            return rm;
          }
        }

      }
    }



    public string delete(FacturaModel model)
    {
      try
      {
        using (var exchangeDb = new ExchangeDBContext())
        {
          using (var eliminarCompra = exchangeDb.Database.BeginTransaction())
          {
            try
            {



              foreach (var detalle in model.detalleFactura)
              {

                DetalleFactura df = new DetalleFactura();

                df.IdFactura = detalle.IdFactura;
                df.Precio = (decimal)detalle.Precio;
                df.IdCriptomoneda = detalle.IdCriptomoneda;
                df.Cantidad = (decimal)detalle.Cantidad;

                df.CotizacionDolar = detalle.CotizacionDolar;
                df.Comision = detalle.Comision;
                df.PorcentajeGanancia = detalle.PorcentajeGanancia;

                exchangeDb.DetalleFacturas.Remove(df);
                exchangeDb.SaveChanges();
              };
              Factura facturaModel = new Factura();
              facturaModel.IdUsuario = model.idUsuario;
              facturaModel.Fecha = DateTime.Now;
              exchangeDb.Facturas.Remove(facturaModel);
              exchangeDb.SaveChanges();
              eliminarCompra.Commit();
            }
            catch (SqlException se)
            {
              eliminarCompra.Rollback();
              return se.Message;
            }
          }
        }
        return "Se eliminò con exito";
      }
      catch (Exception e)
      {
        return e.Message;
      }
    }
  }
}
