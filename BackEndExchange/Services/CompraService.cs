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


                using (var registrarCompra = exchangeDb.Database.BeginTransaction())
                {
                    try
                    {

                        Factura facturaModel = new Factura();
                        facturaModel.IdUsuario = model.idUsuario;
                        facturaModel.Fecha = DateTime.Now;
                        exchangeDb.Facturas.Add(facturaModel);
                        exchangeDb.SaveChanges();

                        foreach (var detalle in model.detalleFactura)
                        {

                            var df = new DetalleFactura();
                            df.IdCriptomoneda = detalle.idCriptomoneda;
                            df.Precio = (decimal)detalle.precio;
                            df.Cantidad = (decimal)detalle.cantidad;
                            df.IdFactura = facturaModel.IdFactura;
                            exchangeDb.DetalleFacturas.Add(df);
                            exchangeDb.SaveChanges();
                        }

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
                            df.IdCriptomoneda = detalle.idCriptomoneda;
                            df.Precio = (decimal)detalle.precio;
                            df.Cantidad = (decimal)detalle.cantidad;
                            df.IdFactura = detalle.idFactura;
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
