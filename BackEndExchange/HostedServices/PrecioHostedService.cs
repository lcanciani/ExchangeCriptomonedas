using BackEndExchange.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BackEndExchange.HostedServices
{
    public class PrecioHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<PrecioHostedService> _logger;
        
        private Timer _timer;
        //private static IConfiguration config { get; set; }

        public PrecioHostedService( ILogger<PrecioHostedService> logger)
        {
            _logger = logger;

        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
      
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10
                ));

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {

      getPrecioCriptomonedas();
      //Console.Write("llegue al doWork");
      //using (var _ex = new ExchangeDBContext()){

      //  var cripto = _ex.Criptomonedas.Single(d => d.Nombre == "");
      //  _ex.Entry(cripto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

      //  decimal? precioBitcon = getPrecioBitconPesos();
      //      if ( precioBitcon != null)
      //      {
      //          Criptomoneda p = new Criptomoneda();
      //          p.IdCriptomoneda = 1;
      //          p = _ex.Criptomonedas.Find(p.IdCriptomoneda);
      //          p.PrecioCompra = precioBitcon;
      //    _ex.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

      //    //_ex.Criptomonedas.Update(p);
      //    _ex.SaveChanges();

      //      }
      //      }
    }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        private decimal? getPrecioCriptomonedas()
        {
      //var url = $"https://criptoya.com/api/argenbtc/btc/ars/1";

      //var url = $"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
      var url = $"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin%2Cethereum%2Cuniswap%2Ccardano&vs_currencies=usd%2Cusd%2Cusd%2Cusd";
      var request = (HttpWebRequest)WebRequest.Create(url);
      //request.ServerCertificateValidationCallback += request.ServerCertificateValidationCallback;
      request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            var responseBody = objReader.ReadToEnd();
              JObject json = JObject.Parse(responseBody);
              using (var _ex = new ExchangeDBContext())
              {
                foreach (var item in json)
                {



                  var cripto = _ex.Criptomonedas.Single(d => d.Nombre == item.Key);
                  cripto.PrecioCompra = item.Value.Value<decimal?>("usd");
                  _ex.Entry(cripto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                  _ex.SaveChanges();

                  

                }
              }
                            //try 
                            //{ 
                            
                            //    return 1;
                            //}
                            //catch (Exception)
                            //{
                            //    return null;
                            //}
                            //u.Email = precio;

                            //_ex.Usuarios.Update(u);
                            //_ex.SaveChanges();

                            
                        }

                    }
                }
        return 1;
            }
            catch (WebException )
            {
                return null;
            }
        }
        /*
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            config = builder.Build();

            return config.GetConnectionString("DefaultConnection");

        }
        */

    }




}
