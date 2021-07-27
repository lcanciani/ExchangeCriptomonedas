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
      if(getPrecioCriptomonedas() == null)
      {
        using (var _ex = new ExchangeDBContext())
        {
          var error = _ex.Errors.Where(e => e.IdError == 1).FirstOrDefault();
          error.PrecioErrorStatus = 1;
          _ex.Entry(error).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          _ex.SaveChanges();
          return Task.CompletedTask;
        }
      }
      
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10
                ));

        return Task.CompletedTask;
      
      
            
        }
        private void DoWork(object state)
        {

      getPrecioCriptomonedas();
     
    }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        private decimal? getPrecioCriptomonedas()
        {
      using (var _ex = new ExchangeDBContext())
      {
        Error error = new Error();
         error = _ex.Errors.Where(e => e.IdError == 1).FirstOrDefault();
        error.PrecioErrorStatus = 0;
        _ex.Entry(error).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _ex.SaveChanges();
      }

        var urlMala = $"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin%2Cethereum%2Cuniswap%2Ccardano%2Cbinancecoinvs_currencies=usd%2Cusd%2Cusd%2Cusd%2Cusd";
      //probar
      var url = $"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin%2Cethereum%2Cuniswap%2Ccardano%2Cbinancecoin%2Csharering&vs_currencies=usd%2Cusd%2Cusd%2Cusd%2Cusd%2Cusd";
      
      var request = (HttpWebRequest)WebRequest.Create(url);
      
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
                  if(calcularRango((decimal)cripto.PrecioCompra, (decimal)item.Value.Value<decimal?>("usd")) == 1)
                  {
                    cripto.PrecioCompra = item.Value.Value<decimal?>("usd");

                    _ex.Entry(cripto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _ex.SaveChanges();
                  }
                  else
                  {
                    return 0;
                  }

                  

                }
              }   
                        }

                    }
                }
        return 1;
            }
            catch (Exception )
            {
                return null;
            }
        }

    private int calcularRango(decimal precioAnterior, decimal precioActual)
    {
      double precioActualAlto = (double)precioActual * 1.3;
      double precioActualBajo = (double)precioActual * 0.7;
      if( (double)precioAnterior>precioActualBajo && (double)precioAnterior < precioActualAlto)
      {
        return 1;
      }
      return 0;
    }
    }




}
