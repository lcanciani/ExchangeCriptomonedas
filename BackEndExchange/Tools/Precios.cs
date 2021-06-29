using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackEndExchange.Tools
{
    public class Precios
    {

        
        public IEnumerable<string> precioBitcon()
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
                        if (strReader == null) return new string[] { "Esto no anda " };
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            Console.WriteLine(responseBody);
                            return new string[] { "El precio es : " + responseBody };
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                return new string[] { "Esto no anda " + ex.Message };
            }
        }

    }
}
