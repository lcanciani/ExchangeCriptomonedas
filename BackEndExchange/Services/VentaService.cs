using BackEndExchange.Model;
using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Services
{
  public class VentaService: IVentaService
  {
    public RespuestaModel add(ConfirmarVentaModel model)
    {

      RespuestaModel rm = new RespuestaModel();
      using (var exchangeDb = new ExchangeDBContext())
      {
        // double totalFactura = model.PrecioVenta + model.PrecioVenta * (model.Comision + 1);
        

        using (var registrarCompra = exchangeDb.Database.BeginTransaction())
        {
          try
          {

            Billetera billeteraUser = exchangeDb.Billeteras.Where(b => b.IdUsuario == model.IdUsuario &&
            b.IdCriptomoneda == model.IdCriptomoneda).FirstOrDefault();
            var Billetera = new Billetera();
            
            if(billeteraUser != null) { 
            if ((double)billeteraUser.Cantidad >=(double) model.Cantidad)
            {
                
              billeteraUser.Cantidad -= (decimal)model.Cantidad;
              exchangeDb.Entry(billeteraUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Console.WriteLine("AAAAAAAAAA0");
                exchangeDb.SaveChanges();
              

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
               
              dfm.Cantidad = (decimal?)model.Cantidad;
              dfm.Precio = (decimal?)model.PrecioCompra;


                dfm.CotizacionDolar = (decimal) model.CotizacionDolar;
              dfm.Comision = (decimal?)model.Comision;
              dfm.PorcentajeGanancia = (decimal?)model.PorcentajeGanancia;
              exchangeDb.DetalleFacturas.Add(dfm);
              Console.WriteLine("AAAAAAAAAA2");
              exchangeDb.SaveChanges();

                var usuario = exchangeDb.Usuarios.Where(u => u.IdUsuario == model.IdUsuario).FirstOrDefault();
                usuario.SaldoFiatUsuario += (decimal)model.Monto;
              
              
              exchangeDb.SaveChanges();
              registrarCompra.Commit();

              rm.exito = 1;
              return rm;
            }
              else
              {
                rm.exito = 0;
                rm.mensanje = "No posee la cantidad suficiente de esta criptomoneda";
                return rm;
              }
            }

            else
            {
              rm.exito = 0;
              rm.mensanje = "No posee esta criptomoneda";
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
  }
}
