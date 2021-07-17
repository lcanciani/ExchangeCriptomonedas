using BackEndExchange.Model;
using BackEndExchange.Model.Request;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Services
{
    public class CompraService : ICompraService
    {
        

        public void add(Model.Request.FacturaModel model)
        {
            using (var exchangeDb = new ExchangeDBContext())
            {
        double totalFactura = 0;

                using (var registrarCompra = exchangeDb.Database.BeginTransaction())
                {
                    try
                    {

                        Factura facturaModel = new Factura();
                        facturaModel.IdUsuario = model.idUsuario;
                        facturaModel.Fecha = DateTime.Now;
                        exchangeDb.Facturas.Add(facturaModel);
                        

                        foreach (var detalle in model.detalleFactura)
                        {

                            var df = new DetalleFactura();
                            
              df.IdFactura = facturaModel.IdFactura;
              df.Precio = (decimal)detalle.Precio;
              df.IdCriptomoneda = detalle.IdCriptomoneda;
              df.Cantidad = (decimal)detalle.Cantidad;

              df.MontoTotalOperacion = detalle.MontoTotalOperacion;
              df.Comision = detalle.Comision;
              df.PorcentajeGanancia = detalle.PorcentajeGanancia;
              totalFactura = totalFactura + (double)df.MontoTotalOperacion;
                            exchangeDb.DetalleFacturas.Add(df);
              //if()
                            
                        }
            //Billetera b = new Billetera();
            var Billetera = exchangeDb.Billeteras.Single<Billetera>(d => d.IdUsuario == facturaModel.IdUsuario);
              if (totalFactura >= (double) Billetera.SaldoFiat)
            exchangeDb.SaveChanges();
            
                        
                        
                        registrarCompra.Commit();
                        
                    }

                    catch (Exception )
                    {

                        registrarCompra.Rollback();
                        
                    }

                }

            }
        }

        

        public string delete( FacturaModel model)
        {
            try
            {
                using(var exchangeDb = new ExchangeDBContext())
                {
                    using (var eliminarCompra = exchangeDb.Database.BeginTransaction())
                    {
                        try {
                            


                            foreach (var detalle in model.detalleFactura) {

                            DetalleFactura df = new DetalleFactura();

                df.IdFactura = detalle.IdFactura;
                df.Precio = (decimal)detalle.Precio;
                df.IdCriptomoneda = detalle.IdCriptomoneda;
                df.Cantidad = (decimal)detalle.Cantidad;

                df.MontoTotalOperacion = detalle.MontoTotalOperacion;
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
