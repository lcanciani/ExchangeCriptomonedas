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
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            using(var _ex = new ExchangeDBContext()){ 
            decimal? precioBitcon = getPrecioBitconPesos();
            if ( precioBitcon != null)
            {
                Criptomoneda p = new Criptomoneda();
                p.IdCriptomoneda = 1;
                p= _ex.Criptomonedas.Find(p.IdCriptomoneda);
                p.PrecioCompra = precioBitcon;
                
                _ex.Criptomonedas.Update(p);
                _ex.SaveChanges();
                
            }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        private decimal? getPrecioBitconPesos()
        {
            var url = $"https://criptoya.com/api/argenbtc/btc/ars/1";
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
                            string responseBody = objReader.ReadToEnd();
                            string precio = responseBody.Substring(7, 10);
                            try 
                            { 
                            decimal p = decimal.Parse(precio);
                                return p;
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                            //u.Email = precio;

                            //_ex.Usuarios.Update(u);
                            //_ex.SaveChanges();

                            
                        }

                    }
                }
            }
            catch (WebException )
            {
                return 0;
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
