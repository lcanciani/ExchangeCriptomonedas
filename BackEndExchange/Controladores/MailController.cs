using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;
using BackEndExchange.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndExchange.Controladores
{
  [Route("api/[controller]")]
  [ApiController]
  public class MailController : ControllerBase
  {
    private readonly IMailService mailService;
    public MailController(IMailService mailService)
    {
      this.mailService = mailService;
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromBody] MailRequest request)
    {
      RespuestaModel rm = new RespuestaModel();
      try
      {
        await mailService.SendEmailAsync(request);
        rm.exito = 1;
        rm.mensanje = "Mail enviado con éxito!";
        return Ok(rm);
      }
      catch (Exception e)
      {
        rm.exito = 0;
        rm.mensanje = "No se pudo enviar el mail";
        rm.data = e.Message;
        return Ok(rm);
      }

    }
  }
}
