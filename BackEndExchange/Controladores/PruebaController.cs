using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndExchange.Controladores
{
  [Route("api/[controller]")]
  [ApiController]
  public class PruebaController : ControllerBase
  {
    // GET: api/<PruebaController>
    [HttpGet]
    public IActionResult Get()
    {
      var url = $"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
      
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
            if (strReader == null) return Ok("no anda");
            using (StreamReader objReader = new StreamReader(strReader))
            {
              string responseBody = objReader.ReadToEnd();
              string precio = responseBody.Substring(18, 5);
              
              //try
              //{
              //  decimal p = decimal.Parse(precio);
                return Ok(responseBody+" ///" + precio);
              //}
              //catch (Exception)
              //{
              //  return Ok("no se pudo convertir a decimal");
              //}
              //u.Email = precio;

              //_ex.Usuarios.Update(u);
              //_ex.SaveChanges();
              

            }

          }

        }
        
      }
      catch (WebException we)
      {
        return Ok(we.Message);
      }
    }

    // GET api/<PruebaController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<PruebaController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<PruebaController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PruebaController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
